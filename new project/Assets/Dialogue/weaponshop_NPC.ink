INCLUDE globals.ink

-> main

=== main ===
How can I help you?
    * [I want to buy something.]
        We are currently out of stock.
        -> DONE
    * [Do you have any quests?]
        {questDone:I do not require assistance for now.|{questStarted:I have already given you a quest. -> in_quest|Could you get rid of the slimes in my shop? -> not_in_quest}}
        -> DONE
    * [Leave]
        Goodbye.
        -> DONE

-> END

=== in_quest ===
* [What am I supposed to do again?]
    Help me get rid of the slimes in my shop.
    -> DONE
* [Leave]
    Goodbye.
    -> DONE

-> END

=== not_in_quest ===
* [Yes]
    Great!
    ~ questName = "MA1511"
    ~ questStarted = true
    -> DONE
* [No]
    Okay, goodbye.
    -> DONE
    
-> END

