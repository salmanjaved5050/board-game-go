# Board Game - Go
A simplified version of **Go** board game. If a stone, when placed makes a trap for an enemy stone ie. sorrounds the enemy stone from
all four orthognal sides, the enemy stone is captrued. One thing to note is that its perfectly fine to place a stone within and existing 
trap of the opponent stones.

<br />

![Image Sequence_001_0000](https://github.com/salmanjaved5050/board-game-go/assets/50775485/470ca229-2158-4035-8bfd-7a1e8926416b)


# How It Works

## GameBoard
The core gameplay is controlled by **GameBoard** class. It expects a scriptable object of type **GameBoardConfig** and creates a game
board. GameBoardConfig has game board size and some prefabs. It spawns all slots of the game board The **GameBoard** class also has a 
reference to **GameLogic** type object which controls the inner logic of the game. 

<br />

<img width="438" alt="Screenshot 2023-06-06 205003" src="https://github.com/salmanjaved5050/board-game-go/assets/50775485/0994b2fc-1661-4981-9213-7d364ccf5a10">

<br />
<br />

Whenever a player plays a move and a game board slot is clicked i.e. a player wants to place his stone on the board, the **GameBoard** class 
calls the **GameLogic** class to evaluate the player move and see if its valid or not. If its valid then it gets the current state of the game 
board from **GameLogic** and applies to the board itself i.e. update the state of all board slots.

<br />


<img width="419" alt="Screenshot 2023-06-06 211414" src="https://github.com/salmanjaved5050/board-game-go/assets/50775485/3ea82da6-9cc9-4894-abc7-d1a360d33721">

<br />

### GameBoardSlot
This class controls an individual board slot that is basically a 3d object which is spawned. It also implements an interface **IGameBoardSlot**
and contains logic related to the slot being clicked on, highlighted or disabled and more.

## GameLogic
This class controls the logic of complete game round. It manages player scores, which player's turn it is and updates the game state plus
holds a reference to **GameBoardState** type object which stores the current state of the board. It is also responsible for checking 
**Traps** if formed after a player moves and captures enemy stones if they lie within a trap. After a move is validated, the logic
checks if traps are formed by the player who placed the stone. If there are any traps and there lies enemy stones inside those traps
then enemy stones are captrued.

<br />

<img width="759" alt="Screenshot 2023-06-06 211414" src="https://github.com/salmanjaved5050/board-game-go/assets/50775485/cf7b95be-9474-49fc-a24c-9890c72a7dd4">

### SlotTrap
This class manages a single trap that can be formed on any of the four sides of the stone that is placed by a player i.e. **Left, Right, Top**
or **Bottom**. In order to identiy and find the board slots which make a trap, we use **Offsets** approach from the location where player
placed the stone which is the **Origin** of the trap. Offsets for each trap are place in **GameConstants** class along with **Orthognal offsets** 
i.e. orthognal slots location from the **origin** Here the orthognal slots are one position up, down, left and right from the origin location
on board. Each trap also has a target location i.e. enemy stone location and captures it given an enemy stone is placed there indeed.

<br />

<img width="677" alt="Screenshot 2023-06-06 211414" src="https://github.com/salmanjaved5050/board-game-go/assets/50775485/40dc9815-f98f-4f35-8187-98673494bfbe">

## GameBoardState
This class maintains the game board state i.e state of all individual slots. After each move this is updated by the **GameLogic** class.
Whenever retrieved, this will have the latest game state i.e. all board slots and what is their individual state like if they are occupied
or not. If occupied then who is the owner, is it player 1 or two. If none of the player is the owner then its free and a stone can be placed
by any player.


<br />

There's also a unit test written inside **UnitTests** folder by the name of **GameBoardTrapTest** for checking traps functionality and capturing 
enemy stones since its the crux of main logic i.e. capturing enemy players.

