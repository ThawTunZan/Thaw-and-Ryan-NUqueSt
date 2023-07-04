INCLUDE globals.ink


-> main

=== main ===
Hmmm this light seems strange, I wonder what it will do in this...
Oh hey there!
    * [I want to buy something.]
        We are currently out of stock.
        -> DONE
    * [Do you have any quests?]
        {allQuestDone:I do not require assistance for now.|{(!validTime): Thanks for the help! Maybe come another time. |{questStarted:I have already given you a quest. -> in_quest|{(!questPC1101Done): -> PC1101Convo |{(!questPC1201Done): -> PC1201Convo}}}}}
        -> DONE
    * [Leave]
        Goodbye.
        -> DONE
-> END


=== PC1201Convo ===
{(!questDone):
-> start_quest("PC1201", "")
-> END
}
     ~questPC1201Done = true
     ~validTime = false
->END


=== PC1101Convo ===
{(!questDone):
    You must be the new person in town!
    My name is Galileo!
    Do you think you can help me with something?
    *[Sure]
        You see as a physicist myself, I am restless when it comes to discovering new things! I have a fascination in astronomy. Lately, I have been seeing dim lights around one of the planet in our galaxy of this world! 
        I have a good feeling this may be something BIG. Maybe it could be moons, infact FOUR MOONS!!!! 
        Sadly, my eyes are failing me this past few days as I have been looking at too many papers... As a result, I've gotten sore eyes...
        So what do you say? Do you think you can help me go take a look into my telescope outside my house and find out if there are in fact four moons beside the planet Poopiter?
        -> start_quest("PC1101", "Find out if there are moons around the planet Poopiter!")
        -> DONE
    *[Nahhh]
        Awww ok...
        -> DONE
    ->END
    }
    
     WOW! I am actually right! There ARE FOUR moons around the planet POOPITER. The question now is what should i name it hmm.... Anyway, thx for the help!.
     ~questPC1101Done = true
     ~validTime = false
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

