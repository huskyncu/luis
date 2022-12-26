// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.18.1

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EchoBot4.Bots
{
    public class EchoBot : ActivityHandler
    {
        private IBotServices _botServices;

        public EchoBot(IBotServices botServices)
        {
            this._botServices = botServices;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var recognizerResult = await _botServices.Dispatch.RecognizeAsync(turnContext, cancellationToken);
            var topIntent = recognizerResult.GetTopScoringIntent();
            await DispatchToTopIntentAsync(turnContext, topIntent.intent, recognizerResult, cancellationToken);
        }

        private async Task DispatchToTopIntentAsync(ITurnContext<IMessageActivity> turnContext, string intent, RecognizerResult recognizerResult, CancellationToken cancellationToken)
        {
            switch (intent)
            {
                case "訂餐":
                    await turnContext.SendActivityAsync(MessageFactory.Text($"好的請稍等"), cancellationToken);
                    break;
                case "好評":
                    await turnContext.SendActivityAsync(MessageFactory.Text($"感謝您"), cancellationToken);
                    break;
                case "負評":
                    await turnContext.SendActivityAsync(MessageFactory.Text($"不好意思"), cancellationToken);
                    break;
                default:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Dispatch unrecognized intent:{intent}"), cancellationToken);
                    break;
            }
        }


        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
