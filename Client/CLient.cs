using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Client
{
    class CLient
    {
        string BrokerAddress = "demo.thingsboard.io";



      //   string BrokerAddress2 = "broker.hivemq.com";

        MqttClient client;

        string clientId;

        public CLient()

        {

            client = new MqttClient(BrokerAddress); ;

            clientId = client.Connect(Guid.NewGuid().ToString(), "utd2HlPZDjWCCTQgAdmL", "").ToString();

            Start();

        }



        void Start()

        {
           
          
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;


            PublishMessage("");

        }



        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)

        {

            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);

            Console.WriteLine(ReceivedMessage);

        }



        void PublishMessage(string message)

        {

            // whole topic

            string Topic = "v1/devices/me/telemetry";



            // publish a message with QoS 2

            client.Publish(Topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);

        }



        void Disconnect()

        {

            client.Disconnect();

        }


    }
}
