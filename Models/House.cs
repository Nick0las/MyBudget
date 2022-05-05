/**************************************************/
/*           Модель из таблицы House БД           */
/**************************************************/

using System;


namespace MyBudget.Models
{
    internal class House
    {
        public int IdHouse { get; set; }
        public string Sity { get; set; }
        public string Street { get; set; }
        public string NumberHouse { get; set; }
        public int NumberAppartament { get; set; }
    }
}
