/**************************************************/
/*    Модель таблицы Transfer из таблицы БД       */
/**************************************************/

using System;

namespace MyBudget.Models
{
    class Transfer
    {
        public int IdTransfer { get; set; }
        public int IdTypeTransfer {get; set;}
        public int IdCard { get; set; }
        public decimal Summa { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
    }
}
