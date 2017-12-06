using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HelloWorld
{
    public static class Utilities
    {
        public static String LOCALHOST = "192.168.1.120";
        //public static String LOCALHOST = "10.0.3.2";
        public static void toggleSpinner (ActivityIndicator spinner) {
            spinner.IsVisible = !spinner.IsVisible;
            spinner.IsRunning = !spinner.IsRunning;
        }

        public static DateTime FindDateTimeBasedOnSlotNumber(DateTime date, int slotNumber)
        {
            DateTime completeDate;
            if (slotNumber % 2 == 0)
            {
                return completeDate = date.AddHours(slotNumber / 2);
            } else
            {
                return completeDate = date.AddHours(slotNumber / 2).AddMinutes(30);
            }
        }

        public static String FindTimeBaseOnSlotNumber(int slotNumber)
        {
            if (slotNumber % 2 == 0)
            {
                if (slotNumber/2 < 10)
                {
                    return "0" + (slotNumber / 2).ToString() + ":00";
                } else
                {
                    return (slotNumber / 2).ToString() + ":00";
                }
            }
            else
            {
                return (slotNumber / 2) + ":30";
            }
        }

        public static List<int> filterAvailableSlotsBasedOnDuration(List<int> allAvailableSlots, int duration)
        {
            List<int> filteredSlots = new List<int>();
            foreach (int aSlot in allAvailableSlots)
            {
                Boolean flag = true;
                for (int i = aSlot; i <= aSlot + duration; i++) {
                    if (!allAvailableSlots.Contains(i))
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    filteredSlots.Add(aSlot);
                }
            }
            return filteredSlots;
        }
    }
}
