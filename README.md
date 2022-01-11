# Twelve (2017): A board game
This is my first project, it was developed in December 2017

Some rules of the game:
+ If there is a path of free (empty) adjacent cells between two boxes with the same number, these can be merged and the destination cell is replaced by its successor.
+ If there is no path of free adjacent cells between two frames or they do not have the same number, they cannot be merged.
+ A box with a number can always be moved to an empty cell as long as there is a path of free (empty) adjacent cells.
+ In Normal Mode, after each shift, boxes with numbers appear in random free positions (generally cells with values ​​between 1 and 3 inclusive appear). If in the previous turn the player merged two squares, only one number comes up. On the contrary, if the player did not merge two squares, he is penalized and instead of a new square two new squares come out. (This can be extended for larger boards and more squares coming out)
+ In Aggressive Mode every certain number of turns (18 generally, or it can depend on how advanced the game is) the board is randomly disorganized.
+ The objective of the game is to have a cell with the largest possible number. In principle, reaching 12 is quite a difficult goal, but it is possible to reach more!
+ The game ends when the player cannot make any valid moves
