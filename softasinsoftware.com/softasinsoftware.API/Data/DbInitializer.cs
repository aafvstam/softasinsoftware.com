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
                    URL = "https://www.amazon.com/gp/product/B07L755X9G?ie=UTF8&linkCode=ll1&tag=softasinsoftw-20&linkId=9c7f9acb01e042f6aecc912a7903843f&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3jMmGmE",
                    Image = "Elgato Lights.jpg"
                },
                new GearItem
                {
                    Name = "Elgato Cam Link",
                    Description = "With Cam Link 4K, use your DSLR, camcorder or action cam as a professional webcam on your PC or Mac. Stream or record in stunning 1080p60 quality or even 4K at 30 fps. And broadcast live via any platform in no time thanks to ultra-low latency technology. Say goodbye to maxing out your memory card mid-shoot or discovering shoddy shots while editing. When recording with Cam Link 4K, all content is stored directly on your hard drive while real-time, full-screen feedback lets you polish scenes on the spot. Your camera has never felt more powerful.",
                    URL = "https://www.amazon.com/gp/product/B07K3FN5MR?ie=UTF8&th=1&linkCode=ll1&tag=softasinsoftw-20&linkId=fe060a93415221034d3ed18b87700998&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3Mb9vYJ",
                    Image = "Elgato Cam Link.jpg"
                },
                new GearItem
                {
                    Name = "Elgato Stream Deck",
                    Description = "Stream Deck puts 15 LCD keys at your fingertips for ultimate studio control. Simply tap to switch scenes, launch media, tweet your feats and more. Personalize your keys with custom icons or choose from hundreds, and get visual feedback every time you execute a command. With Stream Deck, maximize your production value and focus on what matters most: Your audience. Interface Integrated USB 2.0 cable.",
                    URL = "https://www.amazon.com/gp/product/B06XKNZT1P?ie=UTF8&linkCode=ll1&tag=softasinsoftw-20&linkId=9332d55db96f1e9e5937949f47b416b2&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3Mbanwt",
                    Image = "Elgato Stream Deck.jpg"
                },
                new GearItem
                {
                    Name = "Bose QC25",
                    Description = "Bose QuietComfort 25 Acoustic Noise Cancelling Headphones for Apple devices - Black, Wired",
                    URL = "https://www.amazon.com/gp/product/B075V33WMN?ie=UTF8&linkCode=ll1&tag=softasinsoftw-20&linkId=57e7f944f151df2d13947511849e2dd8&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3rvr9OM",
                    Image = "Bose QC25.jpg"
                },
                new GearItem
                {
                    Name = "Rode Procaster Broadcast Dynamic Vocal Microphone",
                    Description = "Designed as a no-compromise microphone for On-Air broadcast use, the Rode Procaster Broadcast Dynamic Vocal Microphone is one of the ultimate broadcast quality mics for the 21st century. Featuring a tight polar pattern and tailored-for-voice frequency response, the Procaster is perfect for every application where a great sounding, rugged microphone with superior ambient noise rejection is demanded. Plus, the Procaster is affordable and features an internal pop-filter to reduce plosives. Includes stand mount, 3/8 inch adaptor and zip pouch. Rode Procaster Features Broadcast quality sound High output dynamic capsule Balanced, low impedance output Internal shock mounting of capsule for low handling noise Internal pop-filter to reduce plosives Robust..",
                    URL = "https://www.amazon.com/gp/product/B001IPUJJI?ie=UTF8&linkCode=ll1&tag=softasinsoftw-20&linkId=d947a0efa5dc224ee2902444b92c56e7&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3EmmnIM",
                    Image = "Rode Procaster Broadcast Dynamic Vocal Microphone.jpg"
                },
                new GearItem
                {
                    Name = "Sony A6400",
                    Description = "Sony Alpha a6400 Mirrorless Camera: Compact APS-C Interchangeable Lens Digital Camera with Real-Time Eye Auto Focus, 4K Video & Flip Up Touchscreen - E Mount Compatible Cameras - ILCE-6400/B Body",
                    URL = "https://www.amazon.com/gp/product/B07MTWVN3M?ie=UTF8&linkCode=ll1&tag=softasinsoftw-20&linkId=2bc1594d9280c9b396e7409a00612923&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3uMVGtG",
                    Image = "Sony A6400.jpg"
                },
                new GearItem
                {
                    Name = "Sigma 30mm 1.4 DC DN Contemporary",
                    Description = "The Sigma 30mm 1.4 DC DN Contemporary is a high performance prime with a large aperture of F1.4 designed for APS-C Mirrorless cameras including Sony E mount and Micro Four Thirds. The Contemporary line is part of Sigma Global Vision and is the perfect blend of image quality and compact size. The large aperture is great for lowlight and for creating depth of field and its small size makes it highly portable. A stepping motor provides fast and accurate autofocus and is highly useful for video work. Like all SGV lenses, each lens is hand crafted in our single factory in Aizu Japan, individually inspected before shipping",
                    URL = "https://www.amazon.com/gp/product/B01C3SCKI6?ie=UTF8&linkCode=ll1&tag=softasinsoftw-20&linkId=c16031fa6874275286e379b230a5d2f7&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3rwGL4V",
                    Image = "Sigma Lens.jpg"
                },
                new GearItem
                {
                    Name = "Akai APC Mini",
                    Description = "Dexterous, versatile, and powerful, the only thing “mini” about this Ableton controller is the size. APC mini is the most portable all-in-one Ableton controller solution with Akai Pro build quality. Engineered specifically for the mobile musician and the desktop producer, APC mini concentrates the essential features of the APC40 mkII, Akai Pro’s flagship Ableton controller, into a compact design that fits in your backpack. The result is a dynamic instrument that empowers you to make music with Ableton Live anywhere.",
                    URL = "https://www.amazon.com/gp/product/B00J3ZCVCS?ie=UTF8&linkCode=ll1&tag=softasinsoftw-20&linkId=7734b51dfa4d8b253bff9354bd0470c9&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3JQqj5B",
                    Image = "Akai APC Mini.jpg"
                },
                new GearItem
                {
                    Name = "Rode PSA1",
                    Description = "Rode PSA 1 Swivel Mount Studio Microphone Boom Arm.",
                    URL = "https://www.amazon.com/gp/product/B001D7UYBO?ie=UTF8&linkCode=ll1&tag=softasinsoftw-20&linkId=36369ee365eaad646892fc804af16f46&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3M9ZPhc",
                    Image = "Rode PSA1.jpg"
                },
                new GearItem
                {
                    Name = "Sigma 18-50mm F2.8 DC DN | C voor Sony E",
                    Description = "Sigma 18-50mm F2.8 DC DN Contemporary for Sony E",
                    URL = "https://www.amazon.com/18-50mm-F2-8-DC-Contemporary-Sony/dp/B09JVBB36L?crid=M2A43ZH5H8Y8&keywords=B09JVBB36L&qid=1650204759&sbo=RZvfv%2F%2FHxDF%2BO5021pAnSA%3D%3D&sprefix=b09jvbb36l%2Caps%2C114&sr=8-1&linkCode=ll1&tag=softasinsoftw-20&linkId=674827f8a5eeef6febfed6802268df7c&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/37ugKfs",
                    URLAmazonNL = "https://www.amazon.nl/Sigma-18-50mm-F2-8-voor-Sony/dp/B09JVBB36L?crid=1QNF2AOD25KHZ&keywords=sigma+18-50mm+f2.8&qid=1650204039&sprefix=sigma%2Caps%2C61&sr=8-1&linkCode=ll1&tag=softasinsoftw-21&linkId=b9bbc299ef3a300f81f47269623ff29a&language=nl_NL&ref_=as_li_ss_tl",
                    ShortURLAmazonNL = "https://amzn.to/38QuDVD",
                    Image = "Sigma1850mm.jpg"
                },
                new GearItem
                {
                    Name = "Behringer XENYX 1204USB",
                    Description = "Behringer XENYX 1204USB Premium 12-Input 2/2-Bus Mixer with XENYX Mic Preamps and Compressors, British EQ and USB/Audio Interface",
                    URL = "https://www.amazon.com/gp/product/B00871VO5Y?ie=UTF8&linkCode=ll1&tag=softasinsoftw-20&linkId=f440b109e81b2540cc203a0d1e3bde61&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3vrSh2z",
                    Image = "BehringerXENYX.jpg"
                },
                new GearItem
                {
                    Name = "Rode PSM1",
                    Description = "Rode PSM1 Shock Mount For Podcaster, Procaster, PSA1, and DS1 Microphones",
                    URL = "https://www.amazon.com/gp/product/B000WA8KYG?ie=UTF8&linkCode=ll1&tag=softasinsoftw-20&linkId=dc88255c27fdaf5394923b620f6f8fcd&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3rvrNfa",
                    Image = "RodePSM1.jpg"
                },
                new GearItem
                {
                    Name = "DBX286s microphone pre-amplifier",
                    Description = "The dbx 286s is a full featured channel strip processor that delivers a studio quality microphone/instrument preamplifier and four processors that can be used independently or in any combination. Why mic up vocals and instruments through a noisy, blurry mixer? The sonically pristine dbx 286s mic preamp has all the features you need, including wide-ranging input gain control, switchable +48V phantom power, and an 80Hz high-pass filter to remove low frequency hum, rumble or wind. Use the patented dbx over Easy compressor to transparently smooth out uneven acoustic tracks or deliver that classic 'in your face' Vocal performance that only a dbx compressor can. Eliminate vocal sibilance and high frequency distortion from instruments such as cymbals with the frequency Tenable De-Esser. Fine-tune the Enhancer HF detail control to add sparkle and Crispness to your tracks and make adjustments to the lf detail control to add fullness and depth to vocals and bass instruments while cleaning up the muddy low midrange frequencies. The separate threshold and ratio controls on the expander/gate allow you to subtly reduce headphone leakage or radically gate noisy guitar amps. And, the dbx 286s offers a full compliment of metering and status LEDs to visually guide you to achieving the right sound.",
                    URL = "https://www.amazon.com/dbx-286s-Microphone-Channel-Processor/dp/B004LWH79A?crid=2NKYDNFGZUJVX&keywords=B004LWH79A&qid=1650205711&sprefix=b004lwh79a%2Caps%2C237&sr=8-1&th=1&linkCode=ll1&tag=softasinsoftw-20&linkId=57eb9fb2286df8e93a254c34ad2acc72&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/38KREt5",
                    Image = "DBX286s.jpg"
                },
                new GearItem
                {
                    Name = "Philips Hue Play",
                    Description = "Connect it to Hue Hub (sold seperately) in order to control lights with your Hue App, Voice or Smart Home device. Compact design, full light experience. Create a vibrant ambiance with the Hue Play light bars. Choose from 16 million colors to experience different light effects. Lay it on the floor, let it stand on the cabinet, or mount it on the back of the TV and paint your wall with light. Sync your lights to music or movies using the Hue Sync app. This base kit provides 2 light points, 1 power supply unit, 2 table stands and 2 TV-mounting supports. Requires the Hue Hub (sold Separately) for the full Hue experience and to take advantage of voice activation. Purchase the Philips Hue Hub (Model: 458471). Search 'Philips Hue Hub' or 'B016H0QZ7I' to find this product on Amazon. Use the Hue dimmer switch, motion sensor, and other smart accessories to control the light bars.",
                    URL = "https://www.amazon.com/gp/product/B07GXB3S7Z?ie=UTF8&linkCode=ll1&tag=softasinsoftw-20&linkId=bd8e06cfd13cea785014d5e72300cc61&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3vhLg44",
                    Image = "PhilipsHuePlay.jpg"
                },
                new GearItem
                {
                    Name = "Corsair K55",
                    Description = "The CORSAIR K55 RGB PRO Gaming Keyboard lights up your desktop with five-zone dynamic RGB backlighting and powers up your gameplay with six easy to set up dedicated macro keys. The K55 RGB PRO is certified for IP42 dust and spill-resistance to stand up to wear, tear, and more, with a detachable palm rest to ensure all-day comfort, win after win.",
                    URL = "https://www.amazon.com/gp/product/B08Y681W3X?ie=UTF8&linkCode=ll1&tag=softasinsoftw-20&linkId=89764a7f88b11fff42a10141bca78c66&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3JNlDgT",
                    Image = "CorsairK55.jpg"
                },
                new GearItem
                {
                    Name = "Microsoft Comfort Mouse 4500 - Lochness Gray",
                    Description = "Work in comfort, stay in control. Work anywhere with this full-featured mouse, which delivers comfort in either hand and the precise control of BlueTrack Technology at a great value.",
                    URL = "https://www.amazon.com/Microsoft-Comfort-Mouse-4500-Lochness/dp/B00902BFGC?crid=2OS6RHMZ2AC0H&keywords=B00902BFGC&qid=1650205351&sprefix=b00902bfgc%2Caps%2C114&sr=8-1&th=1&linkCode=ll1&tag=softasinsoftw-20&linkId=c00f4860d45d1c6fcfe600dcb550e7a1&language=en_US&ref_=as_li_ss_tl",
                    ShortURL = "https://amzn.to/3McuA56",
                    Image = "ComfortMouse.jpg"
                }
            };

            context.GearList.AddRange(gearitems);
            context.SaveChanges();
        }
    }
}
