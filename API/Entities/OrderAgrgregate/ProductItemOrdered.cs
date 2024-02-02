using Microsoft.EntityFrameworkCore;

namespace API.Entities.OrderAggregate;

/// <summary>
/// Represents a product item that is ordered.
/// </summary>
[Owned]
public class ProductItemOrdered
{
    /// <summary>
    /// Gets or sets the ID of the product.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the URL of the product's picture.
    /// </summary>
    public string PictureUrl { get; set; }
}