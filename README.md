# Board Game - Go
A simplified version of **Go** board game. If a stone, when placed makes a trap for an enemy stone ie. sorrounds the enemy stone from
all four orthognal sides, the enemy stone is captrued. One thing to note is that its perfectly fine to place a stone within and existing 
trap of the opponent stones.

<br />


![1](https://github.com/salmanjaved5050/board-game-go/assets/50775485/b378e251-8e25-4ccc-a5a2-99832affaf48)


# How It Works

## GameBoard
The core gameplay is controlled by **GameBoard** class. It expects a scriptable object of type **GameBoardConfig** and creates a game
board. GameBoardConfig has game board size and some prefabs. It spawns all slots of the game board The **GameBoard** class also has a 
reference to **GameLogic** type object which controls the inner logic of the game. 

<br />

![2](https://github.com/salmanjaved5050/board-game-go/assets/50775485/66958ca0-3fb6-4798-a32c-47d4ec0889ca)

<br />
<br />

Whenever a player plays a move and a game board slot is clicked i.e. a player wants to place his stone on the board, the **GameBoard** class 
calls the **GameLogic** class to evaluate the player move and see if its valid or not. If its valid then it gets the current state of the game 
board from **GameLogic** and applies to the board itself i.e. update the state of all board slots.

<br />

![3](https://github.com/salmanjaved5050/board-game-go/assets/50775485/6191349a-0705-47f6-8e56-b6c4b5d23a83)
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

![4](https://github.com/salmanjaved5050/board-game-go/assets/50775485/5571c9fc-2b59-416f-9bc5-2a8b1344951c)


### SlotTrap
This class manages a single trap that can be formed on any of the four sides of the stone that is placed by a player i.e. **Left, Right, Top**
or **Bottom**. In order to identiy and find the board slots which make a trap, we use **Offsets** approach from the location where player
placed the stone which is the **Origin** of the trap. Offsets for each trap are place in **GameConstants** class along with **Orthognal offsets** 
i.e. orthognal slots location from the **origin** Here the orthognal slots are one position up, down, left and right from the origin location
on board. Each trap also has a target location i.e. enemy stone location and captures it given an enemy stone is placed there indeed.

<br />

![5](https://github.com/salmanjaved5050/board-game-go/assets/50775485/d50e2739-91ce-40e6-845c-3d74465cfea6)


## GameBoardState
This class maintains the game board state i.e state of all individual slots. After each move this is updated by the **GameLogic** class.
Whenever retrieved, this will have the latest game state i.e. all board slots and what is their individual state like if they are occupied
or not. If occupied then who is the owner, is it player 1 or two. If none of the player is the owner then its free and a stone can be placed
by any player.


<br />

There's also a unit test written inside **UnitTests** folder by the name of **GameBoardTrapTest** for checking traps functionality and capturing 
enemy stones since its the crux of main logic i.e. capturing enemy players.

