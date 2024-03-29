# HereIStandUnity
A Unity implementation of the board game Here I Stand

Resources taken from https://github.com/bga-his/bga-hereistand

Please support the Here I Stand designers and buy the actual board game. I own a copy of this game. This coding project is personal and not for profit. I am a programmer, but not a game designer. I do not intend to encourage any copyright infringement or piracy here.

----------Development log---------

10/9/2022

Added main menu.

10/11/2022

Added ImportSpace

10/19/2022

Added GM and Deck manager. Added power cards and UI. Need to debug IO.

10/21/2022

Added debaters, UI for VP and turn, logic for GM and Deck. Changed image format. 

10/22/2022

Made UI scalable. Added leaders and reformers. Started Scenario object.

10/24/2022

Added UI for HomeSpace, NewWorld and controlMarker. Started CitySetup and SpaceGM object.

10/26/2022

Added GM2 for observer pattern. Started mandatory event logic.

10/30/2022

Finished turn 1 phase 1, logic for reformation attempt, basic highlight UI.

11/2/2022

Player hands display in another screen. Diplomacy markers, leaders and naval units UI. Improved selecting and playing card logic and UI.

11/4/2022

UI and backend object for negotiation segment, need to check restrictions. 

11/6/2022

Finished negotiation segment, tentative peace segment. Finished spring deployment phase.

11/8/2022

Power cards setup. Finished playing a card for CP.

11/10/2022

Improved UI for what the player needs to do next. More logic on buttons. Need to debug leader movement.

11/12/2022

Finished turn 1 phase 4. Generic method for flipping a space marker. Need to improve VP calculation and redesign status object. 

11/14/2022

Added mandatory events involving changing a ruler. Started UI for filtering different layers of markers. Improved IO for ScriptableObject.

11/18/2022

Started theological debate event.

11/20/2022

Finished theological debate event, counter-reformation, chateaux-building, and HIS065. Improved discarding cards.

11/23/2022

Improved hands display. Finished frontend highlight for CP actions.

11/27/2022

Finished backend logic for CP actions and land units construction. Need to finish 12.2 and naval units construction.

11/29/2022

Debugged diplomacy phase negotiation segment.

12/2/2022 

Peace segment, QOL improvements.

12/5/2022

Seas and naval movements. Move formation over clear/pass part 1. Started scroll view for map and zoom in/out.

12/8/2022

Finished scroll view for all 3 boards. Started logic for winter phase.

12/10/2022

Logic for winter phase and colonize action. Finished 'currently waiting for' UI up to phase 4.

12/13/2022

Logic for explore and conquer action. 

12/17/2022

Boards resized. Various QOL improvements. Unrest. Started frontend for land movements.

12/19/2022

Finished land movements step 1-4.

12/22/2022

Finished interception and field battle.

12/24/2022

Finished land movement and siege except for step 10-11.

12/26/2022

Finished siege. Coded event cards in the 80 range.

12/30/2022

Finished naval movement and combat. Started Declaration of war procedure.

1/4/2023

Finished diplomacy phase and action phase. 

1/6/2023

Refactored other button. Coded event cards in the 100 range.

1/9/2023

Finished resolving explorations in new world phase.

1/11/2023

Finished new world phase and victory phase.

1/15/2023

Debugging. 

1/27/2023

Coded event cards in the 90 range. Added the list for tracking coded cards.

1/29/2023

Coded more event cards and debugged in the 70-80 range.

1/31/2023

Debugging.

2/2/2023

Fixed shallow copy problem. Coded more cards.

2/4/2023

Finished reformers. Coded more cards in the 40 range.

2/6/2023

Finished coding cards for now. Debugged debaters.

2/11/2023

Debugged impulse, CP, and various crashes.

### Coding status for cards

#### All play conditions and options coded:

Home cards: 1, 2, 3, 4, 5, 6

Mandatory cards: 8-16, 18-23

Combat cards: 24-30

Response cards: 31-36

Event cards: 39-46, 48, 50-55, 61, 63-65, 67, 70, 72, 74-83, 85-90, 94, 96, 98-109, 112-114

#### Due to limited time, I don't plan to code all conditions or options in these cards: 

Event cards: 7, 17, 37, 38, 47, 49, 56-60, 62, 66, 68, 69, 71, 73, 84, 91-93, 95, 97, 110, 111, 115, 116

