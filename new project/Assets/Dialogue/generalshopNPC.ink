INCLUDE globals.ink
EXTERNAL QuestCompleted()
-> main

=== main ===
Eve: Hi there! What can I get for you?
    * [I want to buy something.]
        Eve: Sure. This is what we have in stock.
        -> DONE
    * [Do you have any quests?]
        {allQuestDone:Eve: I do not require assistance for now.|{(!EveShopValidTime): Eve: Thanks for the help! Maybe come another time. |{EveShopQuestStarted:Eve: I have already given you a quest. -> in_quest|{(!questDTK1234Done): -> DTK1234Convo |{(!questHSI1000Done): -> HSI1000Convo}}}}}-> DONE
        -> DONE
    * [Leave]
        Eve: Goodbye.
        -> DONE

-> END

=== HSI1000Convo ===
{(!EveShopQuestDone):
    Eve: 
    -> start_quest("HSI1000", "Help me look for a unique flower in the desert, the caves and the forest!")
    ->END
    }
    ~ QuestCompleted()
    Eve: 
     ~questDTK1234Done = true
     ~EveShopValidTime = false
    -> END

=== DTK1234Convo ===
{(!EveShopQuestDone):
    Eve: 
    -> start_quest("DTK1234", "Find 5 leather pieces lying outside of your house.")
    ->END
    }
    ~ QuestCompleted()
    Eve: 
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
  Eve: Oh... okay. -> DONE
-> END