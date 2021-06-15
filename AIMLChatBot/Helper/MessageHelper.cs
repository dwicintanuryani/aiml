using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMLChatBot.Helper
{
    public static class MessageHelper
    {
        //success message
        public static string MSG200001EN = "Message delivered successfully.";
        public static string MSG200001ID = "Pesan telah dikirim.";

        //warning message
        public static string MSG403001EN = "The Request has exceeded time out.";
        public static string MSG403002EN = "The System is currently set to not accept user input.";
        public static string MSG503001EN = "The System is under maintenance.";


        public static string MSG403001ID = "Permintaan telah melewati waktu sesi yang ditentukan (Timeout).";
        public static string MSG403002ID = "Sistem tidak dapat menerima permintaan untuk sementara waktu.";
        public static string MSG503001ID = "Sistem sedang dalam masa pemeliharaan.";



        //information message
        public static string MSG200010EN = "Welcome, any question about mizuho ? We are here to help.";
        public static string MSG200011EN = "Please review our assistance experience";
        public static string MSG200012EN = "Did we help you reach out your inquiry? ";
        public static string MSG200013EN = "Sorry for inconvenience that we did not understand your request";

        public static string MSG200010ID = "Selamat datang, Memiliki pertanyaan mengenai mizuho ? kami hadir untuk membantu anda";
        public static string MSG200011ID = "Mohon untuk menilai rating pelayanan kami.";
        public static string MSG200012ID = "Apakah kami membantu menjawab pertanyaan anda";
        public static string MSG200013ID = "Mohon maaf, kami tidak dapat mengenali permintaan anda";





    }
}
