INCLUDE globals.ink

-> main

=== main ===
Hi! I'm George, Leo's older brother!
How can I help you?
    * [I want to buy something.]
        Sure. This is what we have in stock.
        -> DONE
    * [Do you have any quests?]
        {allQuestDone:I do not require assistance for now. |{(!WeaponSmithValidTime): Thanks for the help. Maybe come another day! |{WeaponSmithQuestStarted:I have already given you a quest. -> in_quest|{(!questMA1511Done): -> MA1511Convo|{(!questMA1512Done): -> MA1512Convo|{(!questMA1508EDone) : ->MA1508EConvo}}}}}}
        -> DONE
    * [Leave]
        Goodbye.
        -> DONE

-> END

=== in_quest ===
* [What am I supposed to do again?]
    {WeaponSmithQuestDesc}
    -> DONE
* [Leave]
    Goodbye.
    -> DONE

-> END

=== MA1511Convo ===
{(!WeaponSmithQuestDone):
Could you get rid of the slimes in my shop?
-> start_quest("MA1511", "Get rid of two slimes!")
-> END
} 
PHEW! Those slimes were KILLING me!. I tried using my Engineering Calculus knowledge like Partial differentiation and power series to try calculating how heavy I have to hit the slimes but those slimes were TOUGH!
Thanks for the help though! 
~questMA1511Done = true 
~WeaponSmithValidTime = false
->END

=== MA1512Convo === 
{(!WeaponSmithQuestDone):
Can you bring me the two iron ores in the first level of the cave in the forest down south? I forgot to bring it back as I was running away from the slimes last night!
-> start_quest("MA1512", "Bring me my two iron ores!")
->END
} Thanks for the iron ores! Now i can start on making weapons using my Laplace transformation and partial differential equations I learnt recently. 
~questMA1512Done = true 
~WeaponSmithValidTime = false
->END

=== MA1508EConvo === 
{(!WeaponSmithQuestDone):
Can you get rid of the slimes on the first level of the cave? They are hindering my work!
-> start_quest ("MA1508E", "Get rid of two slimes on the first level of the cave!")
->END
} Damn there were sooooooo many slimes I was having trouble trying to get the vector space for the slimes' positions...
Thanks a loooooooooooot for the help though! I can finally resume my work! ~questMA1508EDone = true ~validTime = false
~questMA1508EDone = true
~WeaponSmithValidTime = true
->END


=== start_quest(Name, Desc) ===
* [Yes]
    Great!
    ~ WeaponSmithQuestName = Name
    ~ WeaponSmithQuestDesc = Desc
    ~ WeaponSmithQuestStarted = true
    ~ WeaponSmithQuestDone = false
    -> DONE
* [No]
    Sleep with one eye open :)
    -> DONE
    
-> END

