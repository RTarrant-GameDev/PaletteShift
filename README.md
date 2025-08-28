# Palette Shift
#### A 2D platformer game in Unity set in a world that is slowly becoming even more colorful

Palette Shift is a 2D platformer game that I am making using Unity, where you play as a stickman that is trying to survive in a world that is growing more colorful by the day.

The gameplay loop for this game is simple: The player needs to get to the end of the level, with the time and amount of health remaining being the deciding factors in what grade the player passes the level (in a manner not too dissimilar from Cuphead). Between the player and
the exit are obstacles with different colours. When hit by the player, they can have varying effects depending on their color (that are decided by an external Obstacle Effects manager using the Observer pattern), from damaging the player to just simply reversing the 
player's controls. To bypass these effects, the player needs to pick up collectibles that will change their color temporarily, and the color must match that of the obstacle.

When developing this game, I used this opportunity to reinforce my knowledge and understanding of the SOLID principles to make my code easier to maintain and follow, in addition to using a variety of programming design patterns like the Singleton pattern (for essential systems in gameplay where there only needs to be one instance), the Observer pattern (for
handling events that will occur in gameplay, such as colliding with pickups and obstacles), and the MVC pattern (for use in HUD elements such as healthbars and timers).
