using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeakysBlueprinter.Model.Obsolete
{
    public class Blueprint
    {
        public string FileURI { get; private set; }
        public string GridName { get; private set; } = "Unknown";
        public int NumOfGrids { get; private set; } = 0;
        public int NumOfBlocks { get; private set; } = 0;
        public int NumOfDamagedBlocks { get; private set; } = 0;
        public int NumOfCockpits { get; private set; } = 0;
        public string Creator { get; private set; }

        public string Content { get; private set; }

        public List<XElement> Blocks { get; private set; }

        private XElement _data;

        public Blueprint()
        {
            Init(@"C:\Users\Gábor Barát\source\repos\LeakysBlueprinter\LeakysBlueprinter\Data\bp.sbc");
            
        }

        public Blueprint(string fileURI)
        {
            Init(fileURI);
        }

        private void Init(string fileURI)
        {
            // TODO: replace this primitive initial stuff with something useful. A few quick practical feature will do.

            FileURI = fileURI;

            _data = XElement.Load(fileURI);

            IEnumerable<XElement> creators =
                from s in _data.Descendants("ShipBlueprint")
                from n in s.Elements("DisplayName")
                select n;
            Creator = creators.First().Value;

            //var BlocksBase = _data.Element("Definitions").Element("ShipBlueprints").Element("ShipBlueprint").Element("CubeGrids").Element("CubeGrid").Element("CubeBlocks");
            var Blocks = _data.Descendants("CubeGrid").First().Element("CubeBlocks").Elements("MyObjectBuilder_CubeBlock");
            NumOfBlocks = Blocks.Count();

            NumOfDamagedBlocks = (from b in Blocks
                                  where b.Elements("IntegrityPercent").Any()
                                  select b.Elements("IntegrityPercent").First().Value).Count();

            this.Blocks = Blocks.ToList();

            NumOfCockpits = (from b in Blocks
                             where b.Elements("SubtypeName").Single().Value.Contains("Cockpit")
                             select b).Count();


            //_data.Descendants("CubeGrid").First().Element("CubeBlocks");

            /*(from c in Blocks
                 where c.Element("SubtypeName").Value == "DBSmallBlockFighterCockpit"
                 select c).First();
                 */

            /*
            string CockpitOrientation_Forward =
                 (from c in Blocks
                  where c.Element("SubtypeName").Value == "DBSmallBlockFighterCockpit"
                  select c.Element("BlockOrientation").Attribute("Forward").Value).First();

            string CockpitOrientation_Up =
                (from c in Blocks
                 where c.Element("SubtypeName").Value == "DBSmallBlockFighterCockpit"
                 select c.Element("BlockOrientation").Attribute("Up").Value).First();

             int NumOfDownwardThrusters = (from b in Blocks
                                          where b.Element("SubtypeName").Value == "SmallBlockSmallHydrogenThrust"
                                          select b).Count();
                                          */

            /*
            (from b in Blocks
                 where b.Element("SubtypeName").Value == "SmallBlockSmallHydrogenThrust"
                 && b.Element("BlockOrientation").Attribute("Forward").Value == CockpitOrientation_Forward
                 && b.Element("BlockOrientation").Attribute("Up").Value == CockpitOrientation_Up
                 select b).Count();
                 */

            //Content = $"{Environment.NewLine}Cockpit Forward: {CockpitOrientation_Forward}, Cockpit Up: {CockpitOrientation_Up}{Environment.NewLine}Num of upward thrusters:{NumOfDownwardThrusters}";


            var CubeGrids = _data.Descendants("CubeGrid");
            NumOfGrids = CubeGrids.Count();

            if (NumOfGrids == 0)
                return;

            var FirstGridNameElement = CubeGrids.First().Element("DisplayName");

            if (FirstGridNameElement != null)
                GridName = FirstGridNameElement.Value;
        }
    }
}
