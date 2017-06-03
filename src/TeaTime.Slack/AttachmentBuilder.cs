﻿namespace TeaTime.Slack
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Models;
    using Models;

    internal static class AttachmentBuilder
    {
        internal static IEnumerable<Attachment> BuildOptions(IEnumerable<Option> options)
        {
            const int maxButtons = 5;

            var attachments = new List<Attachment>();
            var actions = options.Select(o => CreateButton(o)).ToList();

            //Add initial attachment
            var attachment = new Attachment
            {
                Text = "It's Tea Time baby!",
                CallBackId = "teatime"
            };
            attachment.Actions.AddRange(actions.Take(maxButtons));
            attachments.Add(attachment);

            if(actions.Count < maxButtons)
                return attachments;

            //Add any aditional attachments
            for (var i = 1; i <= (actions.Count) / maxButtons; i++)
            {
                var additionalAttachment = new Attachment
                {
                    CallBackId = "teatime"
                };
                additionalAttachment.Actions.AddRange(actions.Skip(i * maxButtons).Take(maxButtons));
                attachments.Add(additionalAttachment);
            }

            return attachments;
        }

        internal static Action CreateButton(Option option, string name = "tea-option")
        {
            return new Action
            {
                Name = name,
                Text = option.Text,
                Type = "button",
                Value = option.Id.ToString()
            };
        }
    }
}
