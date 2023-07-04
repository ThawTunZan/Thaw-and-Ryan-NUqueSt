INCLUDE globals.ink

-> main

=== main ===
Hi! I'm George, Leo's older brother!
How can I help you?
    * [I want to buy something.]
        We are currently out of stock.
        -> DONE
    * [Do you have any quests?]
        {allQuestDone:I do not require assistance for now. |{(!validTime): Thanks for the help. Maybe come another day! |{questStarted:I have already given you a quest. -> in_quest|{(!questMA1511Done): -> MA1511Convo|{(!questMA1512Done): -> MA1512Convo|{(!questMA1508EDone) : ->MA1508EConvo}}}}}}
        -> DONE
    * [Leave]
        Goodbye.
        -> DONE

-> END

=== in_quest ===
* [What am I supposed to do again?]
    {questDesc}
    -> DONE
* [Leave]
    Goodbye.
    -> DONE

-> END

=== MA1511Convo ===
{(!questDone):
Could you get rid of the slimes in my shop?
-> start_quest("MA1511", "Get rid of two slimes!")
-> END
} 
PHEW! Those slimes were KILLING me!. I tried using my Engineering Calculus knowledge like Partial differentiation and power series to try calculating how heavy I have to hit the slimes but those slimes were TOUGH!
Thanks for the help though! 
~questMA1511Done = true 
~validTime = false
->END

=== MA1512Convo === 
{(!questDone):
Can you bring me the two iron ores in the first level of the cave in the forest down south? I forgot to bring it back as I was running away from the slimes last night!
-> start_quest("MA1512", "Bring me my two iron ores!")
->END
} Thanks for the iron ores! Now i can start on making weapons using my Laplace transformation and partial differential equations I learnt recently. 
~questMA1512Done = true 
~validTime = false
->END

=== MA1508EConvo === 
{(!questDone):
Can you get rid of the slimes on the first level of the cave? They are hindering my work!
-> start_quest ("MA1508E", "Get rid of two slimes on the first level of the cave!")
->END
} Damn there were sooooooo many slimes I was having trouble trying to get the vector space for the slimes' positions...
Thanks a loooooooooooot for the help though! I can finally resume my work! ~questMA1508EDone = true ~validTime = false
~questMA1508EDone = true
~validTime = true
->END


=== start_quest(Name, Desc) ===
* [Yes]
    Great!
    ~ questName = Name
    ~ questDesc = Desc
    ~ questStarted = true
    ~ questDone = false
    -> DONE
* [No]
    Sleep with one eye open :)
    -> DONE
    
-> END

