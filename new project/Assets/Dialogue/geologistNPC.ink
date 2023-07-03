
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

=== GESS1001Convo ===
Hey! Thanks for the help with the previous quest! 
I wondered who made that tomestone... My guess is on this WHOLESOME community called RAG
Anyway, do you think you can help me with exploring the second level of the cave? 
The place is a little bit too dangerous for me but I heard there is a corpse of a weird animal there! 
It must have died from the S/U Monster
Do you think you can help me find out what it is?
-> start_quest("GESS1001", "Inspect a suspicious corpse in the second level of the cave")
-> END


=== HSA1000Convo ===
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
    If you stumbled upon a tomestone, let me know!
    So what do you think? Are you up for the task?
    -> start_quest("HSA1000", "Help me find a tomestone in the first level of the cave!")
*[Nahh]
    Awww ok...
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

