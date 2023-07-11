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
    Heyyy. Thanks for helping me discover the 4 moons around POOPiter! Now, I got a new quest for you!
    Do you think you can help me with it?
    Ever since the discovery of the 4 moons I have been wondering about GRAVITY but I feel like I should familiarise with the gravity on our planet here first!
    Since you came recently, the gravity here must feel a bit off for you! Do you think you could help me with measuring the acceleration due to gravity here? 
    You can do that by observing the rate at which the apple falls from one of the trees in the forest westof this village!
    So...what do you say? You think you can help me with it?
    -> start_quest("PC1201", "Find the acceleration due to gravity on this planet by observing one of the trees in the West Forest!")
    -> END
    }
    WOW! It seems like you have knack for this! I shall give you the name Sir Issac Newton :) 
     ~questPC1201Done = true
     ~validTime = false
->END


=== PC1101Convo ===
{(!questDone):
    You must be the new person in town!
    My name is Galileo!
    You see as a physicist myself, I am restless when it comes to discovering new things! I have afascination in astronomy. Lately, I have been seeing dim lights around one of the planet in ourgalaxy of this world! 
    I have a good feeling this may be something BIG. Maybe it could be moons, infact FOUR MOONS!!!! 
    Sadly, my eyes are failing me this past few days as I have been looking at too many papers... As aresult, I've gotten sore eyes...
    So what do you say? Do you think you can help me go take a look into my telescope outside my house andfind out if there are in fact four moons beside the planet Poopiter?
    -> start_quest("PC1101", "Find out if there are moons around the planet Poopiter!")
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

