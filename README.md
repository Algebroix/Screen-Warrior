# Screen Warrior

## Introduction

Screen Warrior is Work in Progress project that has a goal of showcasing quality of our code. Besides parts with "TODO", code in directory "Done" is refactored, optimized and commented, but not always final. Since this project is only about code, all parts that impact graphics are placeholders and they are easily replacable. Scripts outside of "Done" directory should be working and have no bugs but they may be unoptimized, have a bad structure and are mostly uncommented. These should be treated as quick prototypes and will be replaced by good, clean code in the future. Currently there is no game flow coded and some of planned features are missing, they will be added in the future.

## Platform

Screen Warrior was tested on Windows and Android and works with mouse or touchscreen.

## Tutorial

### Movement

Player can be moved by holding left mouse button or touching the screen and moving.

![](https://media.giphy.com/media/M29kcbagAzY9jZxW2V/giphy.gif)


Camera is static, white edges are blocking player movement.

![](https://media.giphy.com/media/RzU8esamHZQGPA92K5/giphy.gif)


There can be more than one player entities at once. Each one can be controlled. Player that the focus is currently on has area of effect shown, others are half-transparent.

![](https://media.giphy.com/media/uVIXEVvObJRWrvqZNh/giphy.gif)

### Health

Red objects damage player. When player is damaged, he loses 1 hp point (Shown both on player and lower screen edge). He also becomes invulnerable for short duration. While in this state, player can't take damage.

![](https://media.giphy.com/media/jakpnaUFuVQkcDdDYV/giphy.gif)


Magenta objects are enemies. Enemies can shoot bullets:

![](https://media.giphy.com/media/LtQMuTq7nAMHCZaQo8/giphy.gif)


They can also cast hits. While hitting area is half-transparent attack is telegraphed showing where it will appear. It deals damage after telegraphing.

![](https://media.giphy.com/media/17xQBBt6hjCyTJsQTW/giphy.gif)


Green squares are healing orbs. Collect one to heal 2 hp.

![](https://media.giphy.com/media/0UF34IXj7VGTLs6Vhh/giphy.gif)


Purple squares are life orbs. Collect one to create new player, also adding new life. Bottom hp bar always shows active player's hp.

![](https://media.giphy.com/media/shq2ONl7pVqvuiNWTO/giphy.gif)


When player dies and there are other players, it disappears.

![](https://media.giphy.com/media/wOTk2qnFOzEEAdPvl7/giphy.gif)


When player dies and there are no other players, it doesn't disappear. Only life is lost.

![](https://media.giphy.com/media/lamEUAiRY3r4PssG6Z/giphy.gif)


Orange square is merge area. Put 2 players in there to remove one without losing life. It is easier to keep one player alive than two.

![](https://media.giphy.com/media/UbG5LUwu3zemoLR5gF/giphy.gif)


### Combat

Player's attack power is higher when constantly moving fast enough with a max power cap. Fast direction changes reset power. Power scales with fluid move time rather than velocity because it allows more interesting movements to be effective. Power is shown in White->Yellow->Red scale.

![](https://media.giphy.com/media/nZttCoPdFiAaWe6CO9/giphy.gif)


Player can attack objects that are parts of big enemies nad have collisions. When such part is attacked it becomes invulnerable for a short duration. Attacking a part that can hurt player results in taking damage and not dealing damage.

![](https://media.giphy.com/media/lQHZl6OPpDR32azFf3/giphy.gif)


Player can also attack enemies. Attacking with low power is not effective

![](https://media.giphy.com/media/HeyyzfNF7KTFQMz00v/giphy.gif)


Attacking with high power is more effective. Enemy is destroyed after taking enough damage. Destroying enemies give score points (Upper left corner).

![](https://media.giphy.com/media/KBvNPluVgXpis29RiW/giphy.gif)
