INCLUDE globals.ink

-> main

=== main ===
Hi! I'm Sam, I am a fan of PUZZLES!
How can I help you?
    * [I want to buy something.]
        We are currently out of stock.
        -> DONE
    * [Do you have any quests?]
        {allQuestDone:I do not require assistance for now. |{(!NerdValidTime): Thanks for the help. Maybe come another day! |{NerdQuestStarted:I have already given you a quest. -> in_quest|{(!questCS1010Done): -> CS1010Convo|{(!questCS1231Done): -> CS1231Convo|{(!questCS2030Done) : ->CS2030Convo |{(!questCS2040Done): ->CS2040Convo}}}}}}}
        -> DONE
    * [Leave]
        Goodbye.
        -> DONE

-> END

=== in_quest ===
* [What am I supposed to do again?]
    {NerdQuestDesc}
    -> DONE
* [Leave]
    Goodbye.
    -> DONE

-> END

=== CS2040Convo ===
{(!NerdQuestDone):
HELLLLLLLLLLLO. The maze has been so fun to explore after you helped me solve it.
After the maze I discovered something new! There seems to be a whole new floor in the cave with weird moveable rocks. 
I suspect you have to form some kind of configuration. If I remember correctly, it was labelled as N-queens or something
Do you think you can take a look for me?
-> start_quest("CS2040", "Go to fifth floor of cave and solve the N-queen puzzle!")
->END
}

WOW. The puzzle was the hardest one I have seen and you were able to solve it!. You really are amazing!
I should tell other villagers about how you are a great puzzle solver! Maybe the mystery of the S/U Monster will finally be solved!
~questCS2040Done = true
~NerdValidTime = false
->END


=== CS2030Convo ===
{(!NerdQuestDone):
Hey it's you again, my fav fellow quest enthusiast <3! 
Yesterday, I was at on the fourth level of the cave looking for clues for more puzzles using my trusty magnifying glass and I discovered a strange house!
This house had strange notes on the ground and I couldn't read it...
-> start_quest("CS2030", "Help me solve the maze in the fourth floor of the cave")
-> END
} 
SHEEEEESH how did you solve the maze?? I was completely lost inside yesterday. I thought it was a BOX!
Anyway, thanks for solving the maze and telling me the correct route!

~questCS2030Done = true 
~NerdValidTime = false
->END

=== CS1231Convo === 
{(!NerdQuestDone):
Bruhhhhhhhhhhhhhhhhhhhh. You did an amazing job with the puzzle previously, mr yi long ma!
Anyway, theres a puzzle that I have so much trouble solving. It is such a brain teaser!
I will need you to go to the third level of the cave and solve a puzzle for me!
Do you think you can help me with it?
-> start_quest("CS1231", "Go to third floor of the cave and solve a puzzle!")
->END
} 
WOW! You breeze through those logic question like nothing!     
Thanks for the help! My brain is a bit too fuzzy for this... @_@
~questCS1231Done = true 
~NerdValidTime = false
->END

=== CS1010Convo === 
{(!NerdQuestDone):
Hey You must be the new kid in town! Have you explore the cave south of the village?
There seems to be a lot of puzzles inside it. Lately, I have trouble solving a puzzle. It's something to do with some lever mechanics that works with something to do with AND Gates and For LOOPS... Hmmm
Do you think you can help me solve it?
-> start_quest("CS1010", "Help me solve the puzzle in the second floor of the cave down south")
->END
} #Quest completed response
~questCS1010Done = true
~NerdValidTime = false
->END


=== start_quest(Name, Desc) ===
* [Yes]
    Great!
    ~ NerdQuestName = Name
    ~ NerdQuestDesc = Desc
    ~ NerdQuestStarted = true
    ~ NerdQuestDone = false
    -> DONE
* [No]
    Sleep with one eye open :)
    -> DONE
    
-> END