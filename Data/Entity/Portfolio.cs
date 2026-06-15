using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myTradingApp.Data.Entity;

[Table("portfolios")]
[Index("StockId", Name = "stock_id")]
[Index("UserId", "StockId", Name = "unique_user_stock", IsUnique = true)]
public partial class Portfolio
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("stock_id")]
    public int? StockId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [ForeignKey("StockId")]
    [InverseProperty("Portfolios")]
    public virtual Stock? Stock { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Portfolios")]
    public virtual User? User { get; set; }
}
