using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace kozvetito.Areas.allas.Models
{
    public class PayLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Guid UId { get; set; }

        [Required]
        public string Trid { get; set; }

        public string UserEmail { get; set; }

        public string UserPhone { get; set; }

        [Required]
        public string Csomag { get; set; }

        public string Product { get; set; }

        [NotMapped]
        public XElement ProductXml 
        {
            get { return XElement.Parse(Product); }
            set { Product = value.ToString(); }
        }

        [Required]
        public string PaymentMethod { get; set; }

        public string Currency { get; set; }

        public string Szamla { get; set; }

        [NotMapped]
        public XElement SzamlaXml
        {
            get { return XElement.Parse(Szamla); }
            set { Szamla = value.ToString(); }
        }

        [Required]
        public DateTime DateTimeGMT { get; set; }

        public string PayRequest { get; set; }

        [NotMapped]
        public XElement PayRequestXml
        {
            get { return XElement.Parse(PayRequest); }
            set { PayRequest = value.ToString(); }
        }

        public string PaymentDetails { get; set; }

        [NotMapped]
        public XElement PaymentDetailsXml
        {
            get { return XElement.Parse(PaymentDetails); }
            set { PaymentDetails = value.ToString(); }
        }

        public bool Elfogadva { get; set; }
    }
}