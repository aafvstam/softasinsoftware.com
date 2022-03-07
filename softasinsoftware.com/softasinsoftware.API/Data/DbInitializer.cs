using softasinsoftware.Shared.Models;

namespace softasinsoftware.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GearDbContext context)
        {
            if (context.GearList.Any())
            {
                // context.GearList.RemoveRange(context.GearList);
                // context.SaveChanges();
                return;
            }

            var gearitems = new GearItem[]
            {
                new GearItem
                {
                    Name = "Elgato Lights",
                    Description = "Elgato Key Light - Professional 2800 lumens Studio Light with desk clamp for Streaming, Recording and Video Conferencing, Temperature and Brightness app-adjustable on Mac, PC, iOS, Android",
                    URL = "https://www.amazon.com/gp/product/B07L755X9G/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B07L755X9G&linkCode=as2&tag=softasinsoftw-20&linkId=73f92f8d3ad91250ce55dcb0a1be4234",
                    ShortURL = "https://amzn.to/32OHVPP",
                    Image = "Elgato Lights.jpg"
                },
                new GearItem
                {
                    Name = "Elgato Cam Link",
                    Description = "Elgato Cam Link 4K, External Camera Capture Card, Stream and Record with DSLR, Camcorder, ActionCam as Webcam in 1080p60, 4K30 for Video Conferencing, Home Office, Gaming, on OBS, Zoom, Teams, PC/Mac",
                    URL = "https://www.amazon.com/gp/product/B07K3FN5MR/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B07K3FN5MR&linkCode=as2&tag=softasinsoftw-20&linkId=51f8a0b288e7a72a98b2e5f02b68fc01",
                    ShortURL = "https://amzn.to/3pPnxXC",
                    Image = "Elgato Cam Link.jpg"
                },
                new GearItem
                {
                    Name = "Elgato Stream Deck",
                    Description = "Stream Deck puts 15 LCD keys at your fingertips for ultimate studio control. Simply tap to switch scenes, launch media, tweet your feats and more. Personalize your keys with custom icons or choose from hundreds, and get visual feedback every time you execute a command. With Stream Deck, maximize your production value and focus on what matters most: Your audience. Interface Integrated USB 2.0 cable.",
                    URL = "https://www.amazon.com/gp/product/B06XKNZT1P/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B06XKNZT1P&linkCode=as2&tag=softasinsoftw-20&linkId=1b42b66ccfc5cefe386fe7b8f98a61e8",
                    ShortURL = "https://amzn.to/3pQy4C0",
                    Image = "Elgato Stream Deck.jpg"
                },
                new GearItem
                {
                    Name = "Bose QC25",
                    Description = "Bose QuietComfort 25 Acoustic Noise Cancelling Headphones for Apple devices - Black, Wired",
                    URL = "https://www.amazon.com/gp/product/B075V33WMN/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B075V33WMN&linkCode=as2&tag=softasinsoftw-20&linkId=4d76cc064ec85803e31d17dda69c00c4",
                    ShortURL = "https://amzn.to/3eOkj0k",
                    Image = "Bose QC25.jpg"
                },
                new GearItem
                {
                    Name = "Rode Procaster Broadcast Dynamic Vocal Microphone",
                    Description = "The RØDE Procaster is a broadcast-quality dynamic microphone, specifically designed to offer no-compromise performance for voice applications in the broadcast environment. It features a tight polar pattern and tailored-for-voice frequency response. It also features an internal pop filter to minimize plosive sounds that can overload the microphone capsule and distort the audio. Its high output dynamic capsule and balanced low impedance output make it an ideal broadcast and voice over microphone.",
                    URL = "https://www.amazon.com/gp/product/B001IPUJJI/ref=as_li_tl?ie=UTF8&tag=softasinsoftw-20&camp=1789&creative=9325&linkCode=as2&creativeASIN=B001IPUJJI&linkId=738b836b5234dcc7c7f438fafe878d57",
                    ShortURL = "https://amzn.to/3qHU4hz",
                    Image = "Rode Procaster Broadcast Dynamic Vocal Microphone.jpg"
                },
                new GearItem
                {
                    Name = "Sony A6400",
                    Description = "a6400 Mirrorless Interchangeable-Lens Camera (Body Only). Operating Temperature - 32 - 104 degrees F / 0 - 40 degrees C.",
                    URL = "https://www.amazon.com/gp/product/B07MTWVN3M/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B07MTWVN3M&linkCode=as2&tag=softasinsoftw-20&linkId=12b8c7fc88b617927224dca5f907e8e5",
                    ShortURL = "https://amzn.to/32DupPb",
                    Image = "Sony A6400.jpg"
                },
                new GearItem
                {
                    Name = "Sigma Lens",
                    Description = "The Sigma 30mm 1.4 DC DN Contemporary is a high performance prime with a large aperture of F1.4 designed for APS-C Mirrorless cameras including Sony E mount and Micro Four Thirds. The Contemporary line is part of Sigma Global Vision and is the perfect blend of image quality and compact size. The large aperture is great for lowlight and for creating depth of field and its small size makes it highly portable. A stepping motor provides fast and accurate autofocus and is highly useful for video work. Like all SGV lenses, each lens is hand crafted in our single factory in Aizu Japan, individually inspected before shipping",
                    URL = "https://www.amazon.com/gp/product/B01C3SCKI6/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B01C3SCKI6&linkCode=as2&tag=softasinsoftw-20&linkId=f25233cb191e7a0d91b8a608790779df",
                    ShortURL = "https://amzn.to/3qL1noH",
                    Image = "Sigma Lens.jpg"
                },
                new GearItem
                {
                    Name = "Akai APC Mini",
                    Description = "Dexterous, versatile, and powerful, the only thing “mini” about this Ableton controller is the size. APC mini is the most portable all-in-one Ableton controller solution with Akai Pro build quality. Engineered specifically for the mobile musician and the desktop producer, APC mini concentrates the essential features of the APC40 mkII, Akai Pro’s flagship Ableton controller, into a compact design that fits in your backpack. The result is a dynamic instrument that empowers you to make music with Ableton Live anywhere.",
                    URL = "https://www.amazon.com/gp/product/B00J3ZCVCS/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B00J3ZCVCS&linkCode=as2&tag=softasinsoftw-20&linkId=3be8d93e79a65133683c828290232364",
                    ShortURL = "https://amzn.to/3FTZOen",
                    Image = "Akai APC Mini.jpg"
                },
                new GearItem
                {
                    Name = "Rode PSA1",
                    Description = "Rode PSA 1 Swivel Mount Studio Microphone Boom Arm.",
                    URL = "https://www.amazon.com/gp/product/B001D7UYBO/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B001D7UYBO&linkCode=as2&tag=softasinsoftw-20&linkId=3f4449bd7f444ed3e13e86aff231383f",
                    ShortURL = "https://amzn.to/32WPCDw",
                    Image = "Rode PSA1.jpg"
                },
                new GearItem
                {
                    Name = "",
                    Description = "",
                    URL = "",
                    ShortURL = "",
                    Image = ""
                },
                new GearItem
                {
                    Name = "",
                    Description = "",
                    URL = "",
                    ShortURL = "",
                    Image = ""
                },
                new GearItem
                {
                    Name = "",
                    Description = "",
                    URL = "",
                    ShortURL = "",
                    Image = ""
                },
                new GearItem
                {
                    Name = "",
                    Description = "",
                    URL = "",
                    ShortURL = "",
                    Image = ""
                },
                new GearItem
                {
                    Name = "",
                    Description = "",
                    URL = "",
                    ShortURL = "",
                    Image = ""
                },
                new GearItem
                {
                    Name = "",
                    Description = "",
                    URL = "",
                    ShortURL = "",
                    Image = ""
                },
                new GearItem
                {
                    Name = "",
                    Description = "",
                    URL = "",
                    ShortURL = "",
                    Image = ""
                },
                new GearItem
                {
                    Name = "",
                    Description = "",
                    URL = "",
                    ShortURL = "",
                    Image = ""
                },
                new GearItem
                {
                    Name = "",
                    Description = "",
                    URL = "",
                    ShortURL = "",
                    Image = ""
                }
            };

            context.GearList.AddRange(gearitems);
            context.SaveChanges();
        }
    }
}
