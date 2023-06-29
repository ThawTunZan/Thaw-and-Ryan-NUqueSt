
INCLUDE globals.ink

-> main

=== main ===
How can I help you?
    * [I want to buy something.]
        We are currently out of stock.
        -> DONE
    * [Do you have any quests?]
        {allQuestDone:I do not require assistance for now. |{(!validTime): Thanks for the help. Maybe come another day! |{questStarted:I have already given you a quest. -> in_quest|{(!questMA1511Done): Could you get rid of the slimes in my shop? -> start_quest("MA1511", "Get rid of two slimes!") |{(!questMA1512Done): Can you bring me the two iron ores in the first level of the cave in the forest down south? I forgot to bring it back as I was running away from the slimes last night! -> start_quest("MA1512", "Bring me my two iron ores!") |{(!questMA1508EDone) : Can you get rid of the slimes on the first level of the cave? They are hindering my work!}}}}}}
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

