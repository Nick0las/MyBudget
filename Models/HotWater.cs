/**************************************************/
/* Модель таблицы hot_water из таблицы БД        */
/**************************************************/


using System;

namespace MyBudget.Models
{
    class HotWater
    {
        public int IdHotWater { get; set; }
        public int IdHouse { get; set; }
        public string DateHotWater { get; set; }
        public decimal LastMetterHotWater { get; set; }
        public decimal KubHotWater { get; set; }
        public decimal PriceHotWater { get; set; }
        public decimal PayedHotWater { get; set; }
        public decimal DebtHotWater { get; set; }
    }
}
