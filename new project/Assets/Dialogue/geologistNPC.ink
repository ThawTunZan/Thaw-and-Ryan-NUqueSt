
INCLUDE globals.ink

-> main

=== main ===
How can I help you?
    * [I want to buy something.]
        We are currently out of stock.
        -> DONE
    * [Do you have any quests?]
        {allQuestDone:I do not require assistance for now.|{(!validTime): Maybe come another time. |{questStarted:I have already given you a quest. -> in_quest|{(!questHSA1000Done): -> HSA1000Convo}}}}
    * [Leave]
        Goodbye.
        -> DONE

-> END

===HSA1000Convo===
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
    -> start_quest("HSA1000C", "Help me find a tomestone in the first level of the cave!")
*[Nahh]
    Awww ok...
    -> DONE
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

