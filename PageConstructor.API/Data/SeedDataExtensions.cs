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
            Name = "Hero Banner",
            Category = "Layout",
            Label = "🔥 Hero",
            Content = "<section class='hero'><h1>Welcome</h1><p>Start here.</p></section>",
            Css = ".hero { padding: 40px; text-align: center; background: #f5f5f5; }",
            PreviewImageUrl = "https://example.com/images/hero-banner.png",
            IsActive = true,
            Components = new List<Component>
            {
                new()
                {
                    Title = "Hero Header",
                    HtmlContent = "<h1>Welcome</h1>",
                    Css = "h1 { font-size: 3rem; }",
                    PreviewImageUrl = "https://example.com/images/hero-header.png"
                },
                new()
                {
                    Title = "Hero Text",
                    HtmlContent = "<p>Start here.</p>",
                    Css = "p { color: #333; }",
                    PreviewImageUrl = "https://example.com/images/hero-text.png"
                }
            }
        };

        var twoColumnText = new Block
        {
            Name = "Two Column",
            Category = "Layout",
            Label = "🧱 Columns",
            Content = "<div class='row'><div class='col'>Left</div><div class='col'>Right</div></div>",
            Css = ".row { display: flex; gap: 20px; } .col { flex: 1; }",
            PreviewImageUrl = "https://example.com/images/two-column.png",
            IsActive = true,
            Components = new List<Component>
            {
                new()
                {
                    Title = "Left Column",
                    HtmlContent = "<div class='col'>Left</div>",
                    Css = ".col { background: #eee; }",
                    PreviewImageUrl = "https://example.com/images/left-col.png"
                },
                new()
                {
                    Title = "Right Column",
                    HtmlContent = "<div class='col'>Right</div>",
                    Css = ".col { background: #ddd; }",
                    PreviewImageUrl = "https://example.com/images/right-col.png"
                }
            }
        };

        var button = new Block
        {
            Name = "CTA Button",
            Category = "Bootstrap",
            Label = "👉 Button",
            Content = "<a class='btn btn-primary'>Click Me</a>",
            Css = "",
            PreviewImageUrl = "https://example.com/images/button.png",
            IsActive = true,
            Components = new List<Component>
            {
                new()
                {
                    Title = "Primary Button",
                    HtmlContent = "<a class='btn btn-primary'>Click Me</a>",
                    Css = "",
                    PreviewImageUrl = "https://example.com/images/primary-btn.png"
                }
            }
        };

        var imageBlock = new Block
        {
            Name = "Image Block",
            Category = "Content",
            Label = "🖼️ Image",
            Content = "<div class='image-block'><img src='https://via.placeholder.com/150'><p>Caption</p></div>",
            Css = ".image-block { text-align: center; } .image-block img { max-width: 100%; }",
            PreviewImageUrl = "https://example.com/images/image-block.png",
            IsActive = true,
            Components = new List<Component>
            {
                new()
                {
                    Title = "Image",
                    HtmlContent = "<img src='https://via.placeholder.com/150'>",
                    Css = "img { border-radius: 4px; }",
                    PreviewImageUrl = "https://example.com/images/img.png"
                },
                new()
                {
                    Title = "Caption",
                    HtmlContent = "<p>Caption</p>",
                    Css = "p { font-style: italic; }",
                    PreviewImageUrl = "https://example.com/images/caption.png"
                }
            }
        };

        var contactForm = new Block
        {
            Name = "Contact Form",
            Category = "Form",
            Label = "📬 Contact",
            Content = "<form class='contact-form'><input type='text' placeholder='Name'><input type='email' placeholder='Email'><textarea placeholder='Message'></textarea><button type='submit'>Send</button></form>",
            Css = ".contact-form { display: flex; flex-direction: column; gap: 10px; padding: 20px; font-family: sans-serif; } .contact-form input, .contact-form textarea { padding: 10px; border: 1px solid #ccc; } .contact-form button { padding: 10px; background: #007bff; color: white; border: none; }",
            PreviewImageUrl = "https://example.com/images/contact-form.png",
            IsActive = true,
            Components = new List<Component>
            {
                new()
                {
                    Title = "Input Name",
                    HtmlContent = "<input type='text' placeholder='Name'>",
                    Css = "",
                    PreviewImageUrl = "https://example.com/images/input-name.png"
                },
                new()
                {
                    Title = "Input Email",
                    HtmlContent = "<input type='email' placeholder='Email'>",
                    Css = "",
                    PreviewImageUrl = "https://example.com/images/input-email.png"
                },
                new()
                {
                    Title = "Message Box",
                    HtmlContent = "<textarea placeholder='Message'></textarea>",
                    Css = "",
                    PreviewImageUrl = "https://example.com/images/textarea.png"
                },
                new()
                {
                    Title = "Submit Button",
                    HtmlContent = "<button type='submit'>Send</button>",
                    Css = "",
                    PreviewImageUrl = "https://example.com/images/submit-btn.png"
                }
            }
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
