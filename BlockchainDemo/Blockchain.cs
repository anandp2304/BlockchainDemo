using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace BlockchainDemo
{
    [Serializable()]
    public class Blockchain : ISerializable
    {
        public IList<Block> Chain { set; get; }

        public Blockchain()
        {
            InitializeChain();
            AddGenesisBlock();
        }


        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        public Block CreateGenesisBlock()
        {
            return new Block(DateTime.Now, null, "{}");
        }

        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Hash = block.CalculateHash();
            Chain.Add(block);
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }

        public void SerializeBlock(Blockchain blk)
        {
            Stream stream = File.Open("EmployeeInfo.osl", FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            Console.WriteLine("Writing Employee Information");
            bformatter.Serialize(stream, blk);
            stream.Close();
        }

        public void Deserialize()
        {
            Blockchain strm = new Blockchain();
            //Open the file written above and read values from it.
            Stream deSerializeStream = File.Open("EmployeeInfo.osl", FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            //Console.WriteLine("Reading Employee Information");
            strm = (Blockchain)bformatter.Deserialize(deSerializeStream);
            deSerializeStream.Close();
        }
    }
}