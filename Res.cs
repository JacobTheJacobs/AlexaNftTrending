using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlexaNftTrending
{
    class Res
    {
        public string name;
        public string image_url;
        public string created_date;
        public string description;
        public string external_url;
        public string count;
        public Decimal market_cap;
        public string num_owners;
        public Decimal floor_price;
        public Decimal total_volume;
        public Decimal average_price;
        public string sales;
        public Decimal usdVolume;


        //constructor
        public Res(string name,
                   string image_url,
                   string created_date,
                   string description, 
                   string external_url,
                   string count,
                    Decimal market_cap,
                   string num_owners,
                   Decimal floor_price,
                   Decimal total_volume,
                   Decimal average_price,
                   string sales,
                   Decimal usdVolume
                   )
        {
            this.name = name;
            this.image_url = image_url;
            this.created_date = created_date;
            this.description = description;
            this.external_url = external_url;
            this.count = count;
            this.market_cap = market_cap;
            this.num_owners = num_owners;
            this.floor_price = floor_price;
            this.total_volume = total_volume;
            this.average_price = average_price;
            this.sales = sales;
            this.usdVolume = usdVolume;
        }

    }
}
