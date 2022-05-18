/**************************************************/
/* Модель таблицы cold_water из таблицы БД        */
/**************************************************/

using System;

namespace MyBudget.Models
{
    class ColdWater
    {
        public int IdColdWater { get; set; }
        public int IdHouse { get; set; }
        public string DateColdWater { get; set; }        
        public decimal LastMetterColdWater { get; set; }
        public decimal KubColdWater { get; set; }
        public decimal PriceColdWater { get; set; }
        public decimal PayedColdWater { get; set; }
        public decimal DebtColdWater { get; set; }

        // ХЗ что это
        //public static explicit operator int(ColdWater v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
