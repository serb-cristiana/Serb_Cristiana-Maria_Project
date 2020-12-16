namespace MotoLotModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        public int? CustId { get; set; }

        public int? MotorcycleId { get; set; }

        public virtual Catalog Catalog { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
