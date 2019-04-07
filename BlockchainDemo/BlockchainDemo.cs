using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockchainDemo
{
    public partial class BlockchainDemo : Form
    {
        public BlockchainDemo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Blockchain phillyCoin = new Blockchain();
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}"));
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Henry,amount:5}"));

            
            //Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            phillyCoin.SerializeBlock(phillyCoin);

            //Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");

            //Console.WriteLine($"Update amount to 1000");
            phillyCoin.Chain[1].Data = "{sender:Henry,receiver:MaHesh,amount:1000}";

            //Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");

            //Console.WriteLine($"Update hash");
            phillyCoin.Chain[1].Hash = phillyCoin.Chain[1].CalculateHash();

            //Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");

            //Console.WriteLine($"Update the entire chain");
            phillyCoin.Chain[2].PreviousHash = phillyCoin.Chain[1].Hash;
            phillyCoin.Chain[2].Hash = phillyCoin.Chain[2].CalculateHash();
            phillyCoin.Chain[3].PreviousHash = phillyCoin.Chain[2].Hash;
            phillyCoin.Chain[3].Hash = phillyCoin.Chain[3].CalculateHash();

            //Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");

            Console.ReadKey();
        }
    }
}
