using MassTransit.SqlTransport;
using Microsoft.EntityFrameworkCore;
using PageConstructor.Domain.Entities;
using PageConstructor.Persistence.DataContexts;

namespace BookManagement.Api.Data;

public static class SeedDataExtensions
{
    public static async ValueTask InitializeSeedAsync(this IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

        if (!await dbContext.Blocks.AnyAsync())
            await dbContext.SeedBlocks();

        if (dbContext.ChangeTracker.HasChanges())
            await dbContext.SaveChangesAsync();
    }

    private static async ValueTask SeedBlocks(this AppDbContext dbContext)
    {
        var heroBannerBlock = new Block
        {
            //Id = Guid.NewGuid(),
            Name = "Hero Banner",
            Category = "Layout",
            Label = "🔥 Hero",
            Content = "<section class='hero'><h1>Welcome</h1><p>Start here.</p></section>",
            Css = ".hero { padding: 40px; text-align: center; background: #f5f5f5; }",
            PreviewImageUrl = "",
            IsActive = true
        };

        var twoColumnText = new Block
        {
            //Id = Guid.NewGuid(),
            Name = "Two Column",
            Category = "Layout",
            Label = "🧱 Columns",
            Content = "<div class='row'><div class='col'>Left</div><div class='col'>Right</div></div>",
            Css = ".row { display: flex; gap: 20px; } .col { flex: 1; }",
            PreviewImageUrl = "",
            IsActive = true
        };

        var button = new Block
        {
            //Id = Guid.NewGuid(),
            Name = "CTA Button",
            Category = "Bootstrap",
            Label = "👉 Button",
            Content = "<a class='btn btn-primary'>Click Me</a>",
            Css = "",
            PreviewImageUrl = "",
            IsActive = true
        };

        var imageBlock = new Block
        {
            //Id = Guid.NewGuid(),
            Name = "Image Block",
            Category = "Content",
            Label = "🖼️ Image",
            Content = "<div class='image-block'><img src='https://via.placeholder.com/150'><p>Caption</p></div>",
            Css = ".image-block { text-align: center; } .image-block img { max-width: 100%; }",
            PreviewImageUrl = "",
            IsActive = true
        };

        var contactForm = new Block
        {
            //Id = Guid.NewGuid(),
            Name = "Contact Form",
            Category = "Form",
            Label = "📬 Contact",
            Content = "<form class='contact-form'><input type='text' placeholder='Name'><input type='email' placeholder='Email'><textarea placeholder='Message'></textarea><button type='submit'>Send</button></form>",
            Css = ".contact-form { display: flex; flex-direction: column; gap: 10px; padding: 20px; font-family: sans-serif; } .contact-form input, .contact-form textarea { padding: 10px; border: 1px solid #ccc; } .contact-form button { padding: 10px; background: #007bff; color: white; border: none; }",
            PreviewImageUrl = "",
            IsActive = true
        };

        var blocks = new List<Block>
        {
            heroBannerBlock,
            twoColumnText,
            button,
            imageBlock,
            contactForm
        };

        await dbContext.Blocks.AddRangeAsync(blocks);
    }
}