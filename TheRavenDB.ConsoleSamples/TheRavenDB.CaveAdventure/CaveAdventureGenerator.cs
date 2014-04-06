﻿using TheRavenDB.TextAdventure.Core.Entities;
using TheRavenDB.TextAdventure.Core.Entities.Maps;
using TheRavenDB.TextAdventure.Utils.Factories;
using TheRavenDB.TextAdventure.Utils.Generators;

namespace TheRavenDB.CaveAdventure
{
    internal class CaveAdventureGenerator
    {
        public const string ColossalCaveAdventure = "Colossal Cave Adventure";
        private readonly IAdventureGenerator adventureGenerator;

        public CaveAdventureGenerator()
        {
            adventureGenerator = new AdventureGeneratorFactory().Create();
        }

        public void Generate()
        {
            if (adventureGenerator.HasAdventure(ColossalCaveAdventure))
                return;

            var swenugMeetupRoom = new Room
            {
                Id = "rooms/1",
                Name = "Swenug meeting room at Kvadrat",
                Description = "Swenug meeting room at Kvadrat",
                Pathways = new RoomPathways(
                    new RoomPathway{ Direction = "outside", RoomId = "rooms/2"})
            };
            var corridor = new Room
            {
                Id = "rooms/2",
                Name = "Corridor",
                Description = "This is the corridor",
                Pathways = new RoomPathways(
                    new RoomPathway{ Direction = "inside Swenug Meetup room", RoomId = "rooms/1"})
            };
            var programmersRoom = new Room
            {
                Id = "rooms/3",
                Name = "Programmer's Room",
                Description = "Programmers Room"
            };
            var toilet = new Room
            {
                Id = "rooms/4",
                Name = "Toilet",
                Description = "Toilet"
            };
            var cafeteria = new Room
            {
                Id = "rooms/5",
                Name = "Cafeteria",
                Description = "Cafeteria"
            };
            var lobby = new Room()
            {
                Id = "rooms/6",
                Name = "Lobby",
                Description = "Lobby"
            };
            var finale = new Room()
            {
                Id = "rooms/7",
                Description = @"
`.'``````````````````````````````````````````````````````````````````:``+`+``.``````   ""What??!!?"" - A thunderous voice breaks the silence
.:+`:`;,,,`;:,`,``````````````````````````,:;::,````````````````````````+`+.````````   
+:.`'.;',;;,::,;```````````````````````,;;:;::::''.````````````````````;':+;``.`````   
.:,`.``````````,````````````````````.:,,::;::::,::;'````````````````.`+++.+++.,`````   "" You say you still like SQL Server better!!!?
.:.`,..,,,,,,,::``````````````````.,,,,:;::::::;:,,:',````````````````:`:;+`,`.`````   Muahahahaha!!!
;;```````````````````````````````,,,,,::::,::::::,,,,;'``````````````,`:+;+'````````   It is time for your demise servant of the SQL Server God!!""
```````````````````````````````.,,,,,,,,,,,,,,,:,,,,,,:'`````````````,.;.``+`.``````    
``````````````````````````````.,,,,,,,,..,,,,,,,,,,,,,,;+.````````````..````.```````   A blazing ball of flames materializes and engulfs you.
`````````````````````````````.,,,,,,,,.......,,,,,,,,,,,;#.`````````````,`,.````````   
`````````````````````````````,,,,,,...........,,,,,,,,,,,'#`````````````````````````   You die a horrible death.
````````````````````````````:,,,,,...............,,,,,,,,:+#````````````````````````
```````````````````````````:,,,,,,...............,,,,,,,,,;++```````````````````````   R.I.P.
```````````````````````````,,,,,,.................,,,,,,,,:'+.``````````````````````
``````````````````````````.,,,,,,.................,,,,,,,,:;+#``````````````````````
``````````````````````````:,,,,,..................,,,,,,,,,:'+``````````````````````
``````````````````````````:,,,,....................,,,,,,,,:;''`````````````````````
``````````````````````````,,,,,....................,,,,,,,,::'+`````````````````````
``````````````````````````,,,,,.....................,,,,,,,::;+````````..```````````
'`'``````,```````````````,,,,,.......................,,,,,,,:;+:```````;'`:;;:``````
+#+,`..,.:`,.`..`````````:,,,........................,,,,,,,:;+:```````;+::.````````
#:.,:`+'+,;+'':+`````````:,,,........................,,,,,,::;';```````;,;+.````````
,`..,;::,:`'.:`;`````````:,,,....````................,,,,,,::;''```````:,;'.````````
````,````````````````````,,,,,...`````......`........,,,,,,:::''``````,:,`:;````````
``````:.:,,:,:::`````````,,:;;:,,.`````.``````.......,,,,,,:::;;``````,.,`:.````````
`````````````````````````,;''++'+',.``````````.......,,,,,,:::;;````````````````````
````````````````````````::',..,;'':,.`````....,,,:,,,,,,,,,,:;;;`````,,:;;,.,;``````
```````````````````````.:,,,.....,,,..```..,:;+'+++';,,,,,,,:;;'````````````````````
```````````````````````.:,,,,:::::,,,.`...,,:;:;;;::;:,,,,,,:;''````````````````````
```````````````````````,,,,:'''#++:;:.....,,,,......,:;,,,,,:;;'````````````````````
```````````````````````:,,:;;:.+#+.::,...,::::,,:,...,,;:,,,:;;'````````````````````
```````````````````````:,,,,,,,,,,,:,,,,,,::;':#+;;:,,,:,,,,:;;':,``````````````````
```````````````````````,,,.,,....,,,:,..,,,,;:.+#++';,,,,,,,,;;:,;.`````````````````
```````````````````````,,,.,,,,,,,,,,...,,,,:,,,,,::;:,,:,,,,':,.,,`````````````````
```````````````````````,,,........,,,..,,,,,,:,....,,,..,,,,:;,`.,.`````````````````
.``````````````````````,,,........,,,..,,,,,,,,,,,,,,,,,,,,,,:,..,.``+++++++++++````
..`````````````````````,,.........,,,..,,,,,,,,..,,,,,,,,,,,,::..,.``++++++#++++````
..::``..`,,```.````````,,....`....,,...,,,,,..........,,,,,,,,.,.,.``+++++++##++````
..#.+.,+..;```+````````,,..```...,,,....,,:,,.........,,,,,,:,..,.```++++++##+#+````
..#+..++..'``,,:```````,,......,,,,.`.`.,,,,,,..```...,,,,,,:...,,```+++++#+##++````
..#.#.,.+.+.:'`+```````.,.....,.,,:.``..,,,,,,,..```..,,,:,,:,.,.,```++,++++###+````
.....```````````````````,,...,,.,,:,...,::::,,,,......,,,:,::;.,,.```++#;:#+####````
......````,..,.,,```````,,..,.,,,,::,,,:;::,,..,......,,,::::..,,````++########+````
.....```````````````````.,,...,,,,,,,,,,,,,,,,.,,....,,,,,:::..,.````+++#+######````
.....````````````````````.,..,,,,,,......,,,,,,,.....,:,,:::,..,`````++++++#####````
......```````````````````......;#...,,,,,,..,,:,.,,.,,,,,:::,,,``````'++++++++++````
......```````````````````,......;'.. ```...:;:,,,...,,,,,:::,```````````````````````
......```````````````````,......,::..`....,,'';.....,,,,,:;:,``````````````````````.
.......```````````````````......,,,,,,,,.::::,.....,,.,,:;;::``````````````````````.
.......```````````````````,......,....,...,,,,...`.,..,,:;:::``````````````````````.
........```````````````````,,.....,.......,,,......,.,,,:;::,`````````````````````..
.........``````````````````,,............,,.......,..,,:;::::`````````````````````..
.........:,`````````````````,,...................,..,,:;:::,:`````````````````````..
.......++++':```````````````.:,.................,,,,,:;::::,,`````````````````````..
......;'::;''.```````````````,,..,.............,,,,,:;:;::,,:#``````````````````....
......+'+++,.'````````:`'+;`;;:,...``..........,:::;;:;:::,,:@````'```,`'`,;``......
......+':;,`+'````````';`;;`;+`;,..``...`..,.,::;;::::,,:,,:;'.```:;:'';;':''''+####
......;'...;''````````;,``:````:',,........,:;';;;::,,,,,,,,'..```````````````.#..#.
.......'++++'.````````.`';``'``;:'::,,,,,,:'''';;::,,,,,,,,;..:,``````````````.#..#.
........'+++,``````````,;;:`:``,,:;'';;;'++'++;::,,,..,,,,,``,,'#`````````````..##+.
............````````````````````,,::::;;''';;:::,,.....,,,``.`.###`````````````.....
.......:::::;.``````````````````,,,:::;;;;;:::,,.......,,``;..#####+````````````....
.......`...`:````````````````````,,,::;:;:::,,.........,```..+######,```````````....
...........``````````````````````,,,,,,,:,,,............`:;,;########'``````````....
..........```````````````````````,,,.,,,,,.............`,;:;##########+````````.....
.........```````````````````````+,,...................``;;;#########+##+;``````.....
.........```````````````````````#,,..................`.`:.###########+##++`````.....
..........````````````````````:#@,,.........```.....``.;`.###############++.```.....
..........```````````````````+##@,,,......`````....``'.``##################+;.......
............```````````````;###@@:,,,......```....``;.``+###############+##+++`.....
..........```````````````:#####@@;,,,.......`.....`:`,`:######################+`....
..........`````````````:+######@@:,,,,...........`.;:`,#######################++....
.............`.``````:+########@@:,,,..........+`````.#########################++,..
...............````:###########@@::,........;##`````.######################++###++;.
...::'............###########'#@#':,,,...,####`````.##############################+;
...;;+.+''',.:;:+###########'#####+::'+######:``.`.###########################+###+#
...;,;.'+':,,;'+############.@##############+.,,`.#################################+
...::,........#############+,###############..'..##############################+####
.............+#############.,##############.`;,`################################+###
............;#############+,,#############;..;.####################################+
...........`##############,+#############+....######################################
...........+##############,.#############.:,.+######################################
...........##############':,############:.;.+#######################################
..........+##############,,############+.;.,########################################
..........###############,.############.:+.#########################################"
            };
            adventureGenerator.AddRooms(swenugMeetupRoom, corridor, programmersRoom, cafeteria, toilet, lobby, finale);

            var colossalCaveAdventure = new Adventure
            {
                Name = ColossalCaveAdventure,
                Map = new Map { StartingRoom = swenugMeetupRoom}
            };
            adventureGenerator.CreateAdventure(colossalCaveAdventure);
        }
    }
}