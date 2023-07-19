INCLUDE globals.ink
EXTERNAL QuestCompleted()
-> main

=== main ===
Eve: Hi there! What can I get for you?
    * [I want to buy something.]
        Eve: Sure. This is what we have in stock.
        -> DONE
    * [Do you have any quests?]
        {allQuestDone:Eve: I do not require assistance for now.|{(!EveShopValidTime): Eve: Thanks for the help! Maybe come another time. |{EveShopQuestStarted:Eve: I have already given you a quest. -> in_quest|{(!questDTK1234Done): -> PC1101Convo |{(!questPC1201Done): -> PC1201Convo}}}}}-> DONE
        -> DONE
    * [Leave]
        Eve: Goodbye.
        -> DONE

-> END

=== DTK1234Convo ===
{(!EveShopQuestDone):
    Eve: You must be the new person in town! My name is Galileo!
    -> start_quest("PC1101", "Find out if there are moons around the planet Poopiter!")
    ->END
    }
    ~ QuestCompleted()
    Eve: WOW! I am actually right! There ARE FOUR moons around the planet POOPITER. The question now is what should i name it hmm.... Anyway, thx for the help!
     ~questDTK1234Done = true
     ~EveShopValidTime = false
    -> END

=== in_quest ===
* [What am I supposed to do again?]
{EveShopQuestDesc}-> DONE
* [Leave]
  Eve: Goodbye.-> DONE

-> END

=== start_quest(Name, Desc) ===
* [Yes]
  Eve: Great!
  ~ EveShopQuestName = Name
  ~ EveShopQuestDesc = Desc
  ~ EveShopQuestStarted = true
  -> DONE
* [No]
  Eve: Sleep with one eye open :)-> DONE
-> END