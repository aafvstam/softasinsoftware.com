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
                    Description = "",
                    URL = "https://www.amazon.com/gp/product/B06XKNZT1P/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B06XKNZT1P&linkCode=as2&tag=softasinsoftw-20&linkId=1b42b66ccfc5cefe386fe7b8f98a61e8",
                    ShortURL = "https://amzn.to/3pQy4C0",
                    Image = "Elgato Stream Deck.jpg"
                },
                new GearItem
                {
                    Name = "Bose QC25",
                    Description = "",
                    URL = "https://www.amazon.com/gp/product/B075V33WMN/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B075V33WMN&linkCode=as2&tag=softasinsoftw-20&linkId=4d76cc064ec85803e31d17dda69c00c4",
                    ShortURL = "https://amzn.to/3eOkj0k",
                    Image = "Bose QC25.jpg"
                },
                new GearItem
                {
                    Name = "Rode Procaster Broadcast Dynamic Vocal Microphone",
                    Description = "",
                    URL = "https://www.amazon.com/gp/product/B001IPUJJI/ref=as_li_tl?ie=UTF8&tag=softasinsoftw-20&camp=1789&creative=9325&linkCode=as2&creativeASIN=B001IPUJJI&linkId=738b836b5234dcc7c7f438fafe878d57",
                    ShortURL = "",
                    Image = "Rode Procaster Broadcast Dynamic Vocal Microphone.jpg"
                },
                new GearItem
                {
                    Name = "Sony A6400",
                    Description = "",
                    URL = "https://www.amazon.com/gp/product/B07MTWVN3M/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B07MTWVN3M&linkCode=as2&tag=softasinsoftw-20&linkId=12b8c7fc88b617927224dca5f907e8e5",
                    ShortURL = "https://amzn.to/32DupPb",
                    Image = ""
                },
                new GearItem
                {
                    Name = "Sigma Lens",
                    Description = "",
                    URL = "https://www.amazon.com/gp/product/B01C3SCKI6/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B01C3SCKI6&linkCode=as2&tag=softasinsoftw-20&linkId=f25233cb191e7a0d91b8a608790779df",
                    ShortURL = "https://amzn.to/3qL1noH",
                    Image = ""
                },
                new GearItem
                {
                    Name = "Akai APC Mini",
                    Description = "",
                    URL = "https://www.amazon.com/gp/product/B00J3ZCVCS/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B00J3ZCVCS&linkCode=as2&tag=softasinsoftw-20&linkId=3be8d93e79a65133683c828290232364",
                    ShortURL = "https://amzn.to/3FTZOen",
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
