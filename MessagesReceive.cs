using System.Collections.Generic;
using System.Net;
using System.Text;

namespace VolnovNotificator
{
    class MessagesReceive
    {
        private static string _domain = "http://prankota.com/";

        private static string GetHttpContent(string query)
        {
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                return webClient.DownloadString(_domain + query);
            }
        }

        public static KeyValuePair<string, string> GetLastMessages()
        {
            return GetMessages("/not.txt");
        }

        private static KeyValuePair<string, string> GetMessages(string query)
        {
            var messagesContent = GetHttpContent(query);
            var messageValuePair = new KeyValuePair<string, string>();
            var message = messagesContent.Split(';');

            switch (message.Length)
            {
                case 2:
                    messageValuePair = new KeyValuePair<string, string>(message[0], message[1]);
                    break;
                case 1:
                    messageValuePair = new KeyValuePair<string, string>(message[0], null);
                    break;
            }

            return messageValuePair;
        }
    }
}