
INCLUDE globals.ink

-> main

=== main ===
Hi! I'm Leo, George's younger brother!
How can I help you?
    * [I want to buy something.]
        We are currently out of stock.
        -> DONE
    * [Do you have any quests?]
        {allQuestDone:I do not require assistance for now.|{(!validTime): Thanks for the help! Maybe come another time. |{questStarted:I have already given you a quest. -> in_quest|{(!questHSA1000Done): -> HSA1000Convo |{(!questGESS1001Done): -> GESS1001Convo |{(!questGEA1000Done): ->GEA1000}}}}}}
        -> DONE
    * [Leave]
        Goodbye.
        -> DONE

-> END

=== GEA1000 ===
{(!questDone):
Hey! Looks like you are getting the hang of it!
Do you think you can help me get a sample of a rock that on the first level of the cave?
It seems like it is the only unique looking rock in that level!
I really need to do some analysis on this rock cuz I have to do some hypothesis testing on it...
What do you think? Are you down for it?
    *[Yes]
        ->start_quest("GEA1000", "Help me go take a look at the unique rock on the first floor of cave and get a sample of it! Give me an iron ore!")
        -> DONE
    *[Nahhh]
        :(
        -> DONE
-> END
}
WOW! Thanks for the help! I can't believe it! This Rock looks amazing!.
I can't imagine what I would do without you! I would have died in the cave...
     ~questGEA1000Done = true
     ~validTime = false
->END

=== GESS1001Convo ===
{(!questDone):
Hey! Thanks for the help with the previous quest! 
I wondered who made that tomestone... My guess is on this WHOLESOME community called RAG
Anyway, do you think you can help me with exploring the second level of the cave? 
The place is a little bit too dangerous for me but I heard there is a corpse of a weird animal there! 
It must have died from the S/U Monster
Do you think you can help me find out what it is?
-> start_quest("GESS1001", "Inspect a suspicious corpse in the second level of the cave")
-> END
}
Brhhh... The details you showed me made me shiver... The possibility of the S/U monster existing seems high...
I have a feeling the S/U Monster comes at the end of the week...
Anyway, thx for the help! See you next time
     ~questGESS1001Done = true
     ~validTime = false
->END


=== HSA1000Convo ===
{(!questDone):
You must be the new person in town!
My name is Tin!
Do you think you can help me with something?
*[Sure]
    I have been working on a project and I would like to know more about a topic
    Do you think anyone has died in the first level of the cave? The one down south!
    I've heard rumours that there was a person that died there!
    The person seemed to have died after contracting a disease called ZEROGPA...
    He was wondering aimlessly in the cave due to the disease affecting his brain, trapping him in the cave.
    Anyway, help me explore around the first level of the cave. 
    If you stumbled upon a tombstone, let me know!
    So what do you think? Are you up for the task?
    -> start_quest("HSA1000", "Help me find a tombstone in the first level of the cave!")
*[Nahh]
    Awww ok...
-> END
}
WOW! For a first timer you are pretty good! Your assistance helped me in confirming that ZEROGPA is a deadly disease. It is also the cause of the death of our lovely dead companion.
Anyway, since you are such a reliable neighbour I will contact you when I need help again! You can do the same as well!
     ~questHSA1000Done = true
     ~validTime = false
     ->END
    
=== in_quest ===
* [What am I supposed to do again?]
    {questDesc}
    -> DONE
* [Leave]
    Goodbye.
    -> DONE

-> END

=== start_quest(Name, Desc) ===
* [Yes]
    Great!
    ~ questName = Name
    ~ questDesc = Desc
    ~ questStarted = true
    -> DONE
* [No]
    Sleep with one eye open :)
    -> DONE
    
-> END

