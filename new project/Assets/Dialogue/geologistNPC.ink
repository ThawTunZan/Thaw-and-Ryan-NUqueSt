
INCLUDE globals.ink
EXTERNAL QuestCompleted()
-> main

=== main ===
Leo: Hi! I'm Leo, George's younger brother!
Leo: How can I help you?
    * [I want to buy something.]
        Leo: We are currently out of stock.
        -> DONE
    * [Do you have any quests?]
        {allQuestDone:Leo: I do not require assistance for now.|{(!GeologistValidTime): Leo: Thanks for the help! Maybe come another time. |{GeologistQuestStarted:Leo: I have already given you a quest. -> in_quest|{(!questHSA1000Done): -> HSA1000Convo |{(!questGESS1001Done): -> GESS1001Convo |{(!questGEA1000Done): ->GEA1000}}}}}}
        -> DONE
    * [Leave]
        Leo: Goodbye.
        -> DONE

-> END

=== GEA1000 ===
{(!GeologistQuestDone):
Leo: Hey! Looks like you are getting the hang of it!
Leo: Do you think you can help me get a sample of a rock that on the first level of the cave?
Leo: It seems like it is the only unique looking rock in that level!
Leo: I really need to do some analysis on this rock cuz I have to do some hypothesis testing on it...
Leo: What do you think? Are you down for it?
->start_quest("GEA1000", "Help me go take a look at the unique rock on the first floor of cave and gea sample of it! Give me an iron ore!")
-> END
}
~ QuestCompleted()
Leo: WOW! Thanks for the help! I can't believe it! This Rock looks amazing!.
Leo: I can't imagine what I would do without you! I would have died in the cave...
     ~questGEA1000Done = true
     ~GeologistValidTime = false
->END

=== GESS1001Convo ===
{(!GeologistQuestDone):
Leo: Hey! Thanks for the help with the previous quest! 
Leo: I wondered who made that tomestone... My guess is on this WHOLESOME community called RAG
Leo: Anyway, do you think you can help me with exploring the second level of the cave? 
Leo: The place is a little bit too dangerous for me but I heard there is a corpse of a weird animal there! 
Leo: It must have died from the S/U Monster
Leo: Do you think you can help me find out what it is?
-> start_quest("GESS1001", "Inspect a suspicious corpse in the second level of the cave")
-> END
}
~ QuestCompleted()
Leo: Bruhhh... The details you showed me made me shiver... The possibility of the S/U monster existing seems high...
Leo: I have a feeling the S/U Monster comes at the end of the week...
Leo: Anyway, thx for the help! See you next time
     ~questGESS1001Done = true
     ~GeologistValidTime = false
->END


=== HSA1000Convo ===
{(!GeologistQuestDone):
Leo: You must be the new person in town!
Leo: My name is Leo!
Leo: I have been working on a project and I would like to know more about a topic
Leo: Do you think anyone has died in the first level of the cave? The one down south!
Leo: I've heard rumours that there was a person that died there!
Leo: The person seemed to have died after contracting a disease called ZEROGPA...
Leo: He was wondering aimlessly in the cave due to the disease affecting his brain, trapping him in the cave.
Leo: Anyway, help me explore around the first level of the cave. 
Leo: If you stumbled upon a tombstone, let me know!
Leo: So what do you think? Are you up for the task?
-> start_quest("HSA1000", "Help me find a tombstone in the first level of the cave!")
-> END
}
~ QuestCompleted()
Leo: WOW! For a first timer you are pretty good! Your assistance helped me in confirming that ZEROGPA is a deadly disease. It is also the cause of the death of our lovely dead companion.
Leo: Anyway, since you are such a reliable neighbour I will contact you when I need help again! You can do the same as well!
     ~questHSA1000Done = true
     ~GeologistValidTime = false
     ->END
    
=== in_quest ===
* [What am I supposed to do again?]
    {GeologistQuestDesc}
    -> DONE
* [Leave]
    Leo: Goodbye.
    -> DONE

-> END

=== start_quest(Name, Desc) ===
* [Yes]
    Great!
    ~ GeologistQuestName = Name
    ~ GeologistQuestDesc = Desc
    ~ GeologistQuestStarted = true
    ~ GeologistQuestDone = false
    -> DONE
* [No]
    Leo: Some other time I guess?
    -> DONE
    
-> END

