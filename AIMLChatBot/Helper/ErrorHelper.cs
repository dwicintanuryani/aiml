using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIMLChatBot.Helper
{
    public static class ErrorHelper
    {
        //developer error
        public static string ERR900001EN = "Correspondent Artificial Logic for {0} is not Ready";
        public static string ERR900002EN = "Correspondent Artificial Setting for {0} is not Ready";
        public static string ERR900003EN = "Correspondent Artificial Configuration for {0} is not Ready";

        public static string ERR900001ID = "Inisialisasi Korespondensi Logika Buatan untuk {0} gagal dimuat";
        public static string ERR900002ID = "Inisialisasi Korespondensi Pengaturan Buatan untuk {0} gagal dimuat";
        public static string ERR900003ID = "Inisialisasi Korespondensi Aturan Buatan untuk {0} gagal dimuat";

        

        //general error
        public static string ERR900000EN = "Virtual Assistant having technical problem , please try again later.";
        public static string ERR900000ID = "Virtual assistant mengalami kendala, silahkan coba beberapa saat lagi.";
    }
}
