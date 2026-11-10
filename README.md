Simulátor Rubikovy kostky (Maturitní práce)

Aplikace pro simulaci skládání Rubikovy kostky 3×3×3 vytvořená v jazyce C# (.NET 10.0, Windows Forms).

1. Datový model kostky (KB1)

Aplikace využívá objektově orientovaný návrh rozdělený do následujících tříd ve jmenném prostoru RubiksCubeSimulator.Models:

CubeColor (enum): Definuje 6 barev kostky (White, Yellow, Green, Blue, Orange, Red).
* FaceType (enum): Označuje 6 stěn kostky dle mezinárodní notace (Up, Down, Front, Back, Left, Right).
* Sticker (třída): Reprezentuje jedno barevné políčko na stěně kostky.
* Face (třída): Reprezentuje jednu stěnu kostky pomocí dvourozměrného pole Sticker[3, 3]. Index [0, 0] odpovídá levému hornímu rohu při přímém pohledu na danou stěnu v 2D rozvinuté síti.
* Move (třída): Uchovává informaci o jednom tahu – otáčenou stěnu (FaceType) a směr otočení (IsClockwise).
* Cube (třída): Spravuje všech 6 stěn v kolekci Dictionary<FaceType, Face>, provádí rotace, náhodné míchání (Scramble) a detekci složeného stavu (IsSolved).

Rozložení stěn v 2D síti a výchozí barvy

U (Up – horní): BíláD (Down – spodní): ŽlutáF (Front – přední): ZelenáB (Back – zadní): ModráL (Left – levá): OranžováR (Right – pravá): Červená2. Logika rotací a míchání. Každé otočení stěny o 90° po směru hodinových ručiček se skládá ze dvou kroků:Transpozice a překlopení samotné matice 3×3 dané stěny: Prvek na pozici [row, col] se přesune na pozici [col, 2 - row].Cyklický posun sousedních hran: Čtyři přilehlé trojice políček na sousedních stěnách si cyklicky předají své barvy s využitím pomocného tříprvkového pole. Otočení proti směru hodinových ručiček (např. U', F') je realizováno trojnásobným zavoláním rotace po směru hodinových ručiček ($3 \times 90^\circ = 270^\circ$), což zaručuje konzistenci datového modelu bez duplikace kódu. Náhodné zamíchání (Scramble) generuje posloupnost náhodných platných tahů z výchozího stavu, díky čemuž je kostka vždy v matematicky složitelném stavu.
