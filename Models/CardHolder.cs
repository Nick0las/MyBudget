/****************************************************/
/* Модель держателей карт после запроса с JOIN к БД */
/****************************************************/

using System;

namespace MyBudget.Models
{
    class CardHolder
    {
        public int Id_User { get; set; }
        public string NameUser { get; set; }
        public string SurnameUser { get; set; }
        public string CardNumber { get; set; }
        public int IdCard { get; set; }
    }
}
