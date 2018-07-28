using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Tinker.model;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Client
{
    public class CLient
    {
        string BrokerAddress = "test.mosquitto.org";

        string BrokerAddress2 = "broker.hivemq.com";

        //string BrokerAddress3 = "35.234.78.60";

        string BrokerAdress4 = "35.234.113.68";

        MqttClient client;

        string clientId;

        public CLient()

        {

            client = new MqttClient(BrokerAdress4); ;


            byte code = client.Connect(Guid.NewGuid().ToString(), "SEw2QfWIIaDzhDCLNt1g", "");


           // byte code = client.Connect(Guid.NewGuid().ToString(), "AwGLAV628da7SxQi8FV1", "");

            //code 0x00 Connected - Successfully connected to ThingsBoard MQTT server.
            //code 0x04 Connection Refused, bad user name or password - Username is empty.
            //code 0x05 Connection Refused, not authorized - Username contains invalid $ACCESS_TOKEN

            if (code == 0x00)
            {
                Console.WriteLine("Client connected to Server node!");
            }
            else
            {
                Console.WriteLine("Connection Refused");
            }

            Start();

        }



       public  void Start()

        {
            try
            {
               // PublishMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Start: Exception thrown: " + ex.Message);
            }


        }



        public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)

        {

            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);

            Console.WriteLine(ReceivedMessage);

        }



        public void PublishMessage(Thingsboard db)

        {

            // whole topic

            //string Topic = "/TestTopic7895798/test";

            string Topic = "v1/devices/me/telemetry";

            var test = new Testklasse();
            test.con = 40.8;
            test.temp = 50.8;


            // publish a message with QoS 2

            client.Publish(Topic, Encoding.UTF8.GetBytes(ToJSON(db)));
            Thread.Sleep(1000);
        }



        void Disconnect()

        {

            client.Disconnect();

        }

        public string ToJSON(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }


    }
}