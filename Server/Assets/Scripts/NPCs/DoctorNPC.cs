using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DoctorNPC : NPCBehaviour
{
    [StoreData]
    Dictionary<string, int> amountOfTimesTalkedToo = new Dictionary<string, int>();
    bool PlayerHasFinishedAllConvo = false;
    [SerializeField] private FlowerManNPC flowerManNpc;
    //Dictionary fizz buzz joke?
    //and make it so player has to spam to be able to talk to him.
    public override NPCBehaviour StartConversation()
    {
        Debug.Log($" 1 \n PlayerHasFinishedAllConvo: {PlayerHasFinishedAllConvo} \n hasInteractedAlready: {hasInteractedAlready}");
        if (PlayerHasFinishedAllConvo)
        {
            conversationList.Clear();
            return this;
        }
        if (!hasInteractedAlready)
        {
            base.StartConversation();
            hasInteractedAlready = false;
        }
        else 
        {
            base.StartConversation();
        }
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            PlayerHasFinishedAllConvo = true;
            if (!PlayerInteraction.hasGivenFlower)
            {
                PlayerInteraction.hasGivenFlower = true;
                PlayerMakeNPCHappier(1);
                flowerManNpc.PlayerMakeNPCSadder(1);
            }
        }
        hasInteractedAlready = true;
        Debug.Log($" 2 \n PlayerHasFinishedAllConvo: {PlayerHasFinishedAllConvo} \n hasInteractedAlready: {hasInteractedAlready}");
        return this;
    }

    public override void OnClientDisconnected(ulong clientID)
    {
        PlayerHasFinishedAllConvo = false;
        base.OnClientDisconnected(clientID);
    }

    private void Awake()
    {
        PlayerHasFinishedAllConvo = false;
        flowerManNpc = FindObjectOfType<FlowerManNPC>();
    }

    //Methods

    #region Miserable
    protected override void Miserable_New()
    {
        conversationList.Add("No no no no NO!");
        conversationList.Add("Why is this happening!");
        conversationList.Add("WHAT??? What do you want???");
        conversationList.Add($"A new face, ugh... you seriously look like a {PlayerInteraction.Username}. Not like I care...");
        conversationList.Add("Sorry I got better things to do then guessing names.");
        conversationList.Add("Wait I got it right? Of course I did.");
        conversationList.Add("Well, since you are here you may as well help me out.");
        conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
        conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
        conversationList.Add("I do really need it. So you MUST grab it you hear me????");
        conversationList.Add("I am feeling quite down, so please bring me good news...");
    }

    protected override void Miserable_PlayerMiserable()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("...");
                conversationList.Add("Ah apologies I was distracted, was thinking what I should do next...");
                conversationList.Add("What? You gave the flower away?? The flower was of upmost importance to me! Science should never be stopped!");
                conversationList.Add("Science is already suffering as it is! You can't be serious! This can push back the village!");
                conversationList.Add("Agh why is everything going wrong, why does it seem like everythings against me! Against science!");
                conversationList.Add("Leave me alone! This will take me some time to recover from me!");
            }
            else
            {
                conversationList.Add("...");
                conversationList.Add("Ah apologies I was distracted, was thinking what I should do next...");
                conversationList.Add("What? You have the flower? Please don't tell me you are joking... no, you do really have it!");
                conversationList.Add("Thank you thank you thank you thank you thank you thank you thank you!!!");
                conversationList.Add("I thought I would struggle for a long time but now with this I can quickly move forward!");
                conversationList.Add("Science is important for the future, and I really believe it will drive the village forward. Yet it seemed so many disagree with me.");
                conversationList.Add("I will proof everyone wrong! Thank you again for your efforts.");
                conversationList.Add("Now I apologise but I will need to put all my concentration to my research!");
            }
        }
        else
        {
            conversationList.Add("This is miserable... at this rate this village won't move forward.");
            conversationList.Add("This is the worst possible day and I have a feeling that it will just get worse...");
            conversationList.Add($"Who are you again? You seem like one troublesome person.");
            conversationList.Add("If you aren't here to help me please leave but if you are then I welcome you. I am having a truly troublesome time.");
            conversationList.Add("You see my research is. how do I say it, just not moving forward. So please, could you help me out? I just need one little thing.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("I am feeling quite down, so please bring me good news...");
        }
    }

    protected override void Miserable_PlayerUnhappy()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("...");
                conversationList.Add("Ah apologies I was distracted, was thinking what I should do next...");
                conversationList.Add("What? You gave the flower away?? The flower was of upmost importance to me! Science should never be stopped!");
                conversationList.Add("Science is already suffering as it is! You can't be serious! This can push back the village!");
                conversationList.Add("Agh why is everything going wrong, why does it seem like everythings against me! Against science!");
                conversationList.Add("Leave me alone! This will take me some time to recover from me!");
            }
            else
            {
                conversationList.Add("...");
                conversationList.Add("Ah apologies I was distracted, was thinking what I should do next...");
                conversationList.Add("What? You have the flower? Please don't tell me you are joking... no, you do really have it!");
                conversationList.Add("Thank you thank you thank you thank you thank you thank you thank you!!!");
                conversationList.Add("I thought I would struggle for a long time but now with this I can quickly move forward!");
                conversationList.Add("Science is important for the future, and I really believe it will drive the village forward. Yet it seemed so many disagree with me.");
                conversationList.Add("I will proof everyone wrong! Thank you again for your efforts.");
                conversationList.Add("Now I apologise but I will need to put all my concentration to my research!");
            }
        }
        else
        {
            conversationList.Add("This is miserable... at this rate this village won't move forward.");
            conversationList.Add("WHAT IS IT! Science is hurting over here!");
            conversationList.Add($"I am already rock bottom. I got the feeling that you are a bit of trouble too...");
            conversationList.Add("I can't even be bothered to try and remember you.");
            conversationList.Add("My research is at the slowest it has ever been, please could you help me? Anyone's help is what's necessary right now.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("I am feeling quite down, so please bring me good news...");
        }
    }

    protected override void Miserable_PlayerNeutral()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("...");
                conversationList.Add("Ah apologies I was distracted, was thinking what I should do next...");
                conversationList.Add("What? You gave the flower away?? The flower was of upmost importance to me! Science should never be stopped!");
                conversationList.Add("Science is already suffering as it is! You can't be serious! This can push back the village!");
                conversationList.Add("Agh why is everything going wrong, why does it seem like everythings against me! Against science!");
                conversationList.Add("Leave me alone! This will take me some time to recover from me!");
            }
            else
            {
                conversationList.Add("...");
                conversationList.Add("Ah apologies I was distracted, was thinking what I should do next...");
                conversationList.Add("What? You have the flower? Please don't tell me you are joking... no, you do really have it!");
                conversationList.Add("Thank you thank you thank you thank you thank you thank you thank you!!!");
                conversationList.Add("I thought I would struggle for a long time but now with this I can quickly move forward!");
                conversationList.Add("Science is important for the future, and I really believe it will drive the village forward. Yet it seemed so many disagree with me.");
                conversationList.Add("I will proof everyone wrong! Thank you again for your efforts.");
                conversationList.Add("Now I apologise but I will need to put all my concentration to my research!");
            }
        }
        else
        {
            conversationList.Add("This is miserable... at this rate this village won't move forward.");
            conversationList.Add("... Please leave me alone... wait..."); ;
            conversationList.Add($"Hold on... isn't this... {PlayerInteraction.Username}? Of course it is I am never wrong! Science is never wrong!");
            conversationList.Add("Noone has been helping me lately! Noone at all!");
            conversationList.Add("Science is what drives this village forward. What drives the future forward. So help out will ya?");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("I am feeling quite down, so please bring me good news...");
        }
    }

    protected override void Miserable_PlayerHappy()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("...");
                conversationList.Add("Ah apologies I was distracted, was thinking what I should do next...");
                conversationList.Add("What? You gave the flower away?? The flower was of upmost importance to me! Science should never be stopped!");
                conversationList.Add("Science is already suffering as it is! You can't be serious! This can push back the village!");
                conversationList.Add("Agh why is everything going wrong, why does it seem like everythings against me! Against science!");
                conversationList.Add("Leave me alone! This will take me some time to recover from me!");
            }
            else
            {
                conversationList.Add("...");
                conversationList.Add("Ah apologies I was distracted, was thinking what I should do next...");
                conversationList.Add("What? You have the flower? Please don't tell me you are joking... no, you do really have it!");
                conversationList.Add("Thank you thank you thank you thank you thank you thank you thank you!!!");
                conversationList.Add("I thought I would struggle for a long time but now with this I can quickly move forward!");
                conversationList.Add("Science is important for the future, and I really believe it will drive the village forward. Yet it seemed so many disagree with me.");
                conversationList.Add("I will proof everyone wrong! Thank you again for your efforts.");
                conversationList.Add("Now I apologise but I will need to put all my concentration to my research!");
            }
        }
        else
        {
            conversationList.Add("This is miserable... at this rate this village won't move forward.");
            conversationList.Add("Who is there?");
            conversationList.Add($"Oh its you {PlayerInteraction.Username}? Apologies, I didn't notice you there at first.");
            conversationList.Add("I am trying to concentrate on the future of the village yet noone is giving me the resource I am asking for!");
            conversationList.Add("It is going quite badly... please could you help me?");
            conversationList.Add("I want to drive science and this village forward.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("I am feeling quite down, so please bring me good news...");
        }
    }

    protected override void Miserable_PlayerTranscendent()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("...");
                conversationList.Add("Ah apologies I was distracted, was thinking what I should do next...");
                conversationList.Add("What? You gave the flower away?? The flower was of upmost importance to me! Science should never be stopped!");
                conversationList.Add("Science is already suffering as it is! You can't be serious! This can push back the village!");
                conversationList.Add("Agh why is everything going wrong, why does it seem like everythings against me! Against science!");
                conversationList.Add("Leave me alone! This will take me some time to recover from me!");
            }
            else
            {
                conversationList.Add("...");
                conversationList.Add("Ah apologies I was distracted, was thinking what I should do next...");
                conversationList.Add("What? You have the flower? Please don't tell me you are joking... no, you do really have it!");
                conversationList.Add("Thank you thank you thank you thank you thank you thank you thank you!!!");
                conversationList.Add("I thought I would struggle for a long time but now with this I can quickly move forward!");
                conversationList.Add("Science is important for the future, and I really believe it will drive the village forward. Yet it seemed so many disagree with me.");
                conversationList.Add("I will proof everyone wrong! Thank you again for your efforts.");
                conversationList.Add("Now I apologise but I will need to put all my concentration to my research!");
            }
        }
        else
        {
            conversationList.Add("This is miserable... at this rate this village won't move forward.");
            conversationList.Add("I feel like today is my lucky day...");
            conversationList.Add($"Ah! Yes! {PlayerInteraction.Username}! How relieved I am to see you.");
            conversationList.Add("You see I have been struggling lately. My research has gone to a complete stop. I need a specific resource yet noone is willing to help me...");
            conversationList.Add("I know you have helped me a lot before, I need you to keep helping me. I want to drive science and this village forward.");
            conversationList.Add("I believe in you.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("I am feeling quite down, so please bring me good news...");
        }
    }
    #endregion

    #region Unhappy
    protected override void Unhappy_New()
    {
        conversationList.Add("Argh...");
        conversationList.Add("This should... do... hopefully...");
        conversationList.Add("What is it?? I am working here!!");
        conversationList.Add($"Ah a new face... you look like a... ugh... I guess a {PlayerInteraction.Username}.");
        conversationList.Add("Sorry I got better things to do then guessing names properly.");
        conversationList.Add("Wait I got it right? Of course I did.");
        conversationList.Add("Well, since you are here you may as well help me out.");
        conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
        conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
        conversationList.Add("I must have it.");
        conversationList.Add("Please try to leave me be until you have it.");
    }

    protected override void Unhappy_PlayerMiserable()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What? You don't have the flower with you? You gave it away??? My research is slowly dying and you decided to give away the flower!");
                conversationList.Add("We need to continue research, we need to evolve, humans need to evolve and the only way is with science!");
                conversationList.Add("Agh I already had a bad feeling today, leave me alone I will need to think my next steps.");
            }
            else
            {
                conversationList.Add("Yes? I am busy... Oh it's you! Are you back with the flower??");
                conversationList.Add("YES! My research can continue at a fast pace again! Thank you so much.");
                conversationList.Add("I want to drive this village forward and you have helped me push it further! Thank you!");
                conversationList.Add("This flower can do so much! I can't be bothered to explain currently but hopefully in the future you will enjoy its benefits!");
                conversationList.Add("You have been really kind, now leave me to my research! I will move humanity forward!");
            }
        }
        else
        {
            conversationList.Add("Agh why has it been so difficult lately.");
            conversationList.Add("Now I am also getting a feeling that it will just get harder.");
            conversationList.Add($"My presumption is my feeling is coming from you. Not sure who you are but I do vaguely remember you.");
            conversationList.Add("I am assuming I must not like you very much. However, you can do something to make me like you.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Please try to leave me be until you have it.");
        }
    }

    protected override void Unhappy_PlayerUnhappy()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What? You don't have the flower with you? You gave it away??? My research is slowly dying and you decided to give away the flower!");
                conversationList.Add("We need to continue research, we need to evolve, humans need to evolve and the only way is with science!");
                conversationList.Add("Agh I already had a bad feeling today, leave me alone I will need to think my next steps.");
            }
            else
            {
                conversationList.Add("Yes? I am busy... Oh it's you! Are you back with the flower??");
                conversationList.Add("YES! My research can continue at a fast pace again! Thank you so much.");
                conversationList.Add("I want to drive this village forward and you have helped me push it further! Thank you!");
                conversationList.Add("This flower can do so much! I can't be bothered to explain currently but hopefully in the future you will enjoy its benefits!");
                conversationList.Add("You have been really kind, now leave me to my research! I will move humanity forward!");
            }
        }
        else
        {
            conversationList.Add("Agh why has it been so difficult lately.");
            conversationList.Add("Listen I am not in the mood.");
            conversationList.Add($"Wait... I think I remember you... but I don't currently have the time or energy to think about your name.");
            conversationList.Add("Research has been slow. I need some help if you could help. You could help me make it quicker.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Please try to leave me be until you have it.");
        }
    }

    protected override void Unhappy_PlayerNeutral()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What? You don't have the flower with you? You gave it away??? My research is slowly dying and you decided to give away the flower!");
                conversationList.Add("We need to continue research, we need to evolve, humans need to evolve and the only way is with science!");
                conversationList.Add("Agh I already had a bad feeling today, leave me alone I will need to think my next steps.");
            }
            else
            {
                conversationList.Add("Yes? I am busy... Oh it's you! Are you back with the flower??");
                conversationList.Add("YES! My research can continue at a fast pace again! Thank you so much.");
                conversationList.Add("I want to drive this village forward and you have helped me push it further! Thank you!");
                conversationList.Add("This flower can do so much! I can't be bothered to explain currently but hopefully in the future you will enjoy its benefits!");
                conversationList.Add("You have been really kind, now leave me to my research! I will move humanity forward!");
            }
        }
        else
        {
            conversationList.Add("Agh why has it been so difficult lately.");
            conversationList.Add("Sorry, but I am currently a bit busy...");
            conversationList.Add($"Oh wait I remember you... what was your name again? It was... {PlayerInteraction.Username}. Yes, that must be it.");
            conversationList.Add("Research has been quite difficult, noone is getting me what I need!");
            conversationList.Add("Listen... mind helping me out? This research could really help the future of this village!");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Please try to leave me be until you have it.");
        }
    }

    protected override void Unhappy_PlayerHappy()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What? You don't have the flower with you? You gave it away??? My research is slowly dying and you decided to give away the flower!");
                conversationList.Add("We need to continue research, we need to evolve, humans need to evolve and the only way is with science!");
                conversationList.Add("Agh I already had a bad feeling today, leave me alone I will need to think my next steps.");
            }
            else
            {
                conversationList.Add("Yes? I am busy... Oh it's you! Are you back with the flower??");
                conversationList.Add("YES! My research can continue at a fast pace again! Thank you so much.");
                conversationList.Add("I want to drive this village forward and you have helped me push it further! Thank you!");
                conversationList.Add("This flower can do so much! I can't be bothered to explain currently but hopefully in the future you will enjoy its benefits!");
                conversationList.Add("You have been really kind, now leave me to my research! I will move humanity forward!");
            }
        }
        else
        {
            conversationList.Add("Agh why has it been so difficult lately.");
            conversationList.Add("... there seems to be a nice presence near me.");
            conversationList.Add($"Ah yes, {PlayerInteraction.Username}. Seems like we do have a friend here.");
            conversationList.Add("Hopefully you been well. I am trying to get through the day.");
            conversationList.Add("People seem to not care about what could happen with the village.");
            conversationList.Add("Listen... mind helping me out? This research could really help the future of this village!");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Please try to leave me be until you have it.");
        }
    }

    protected override void Unhappy_PlayerTranscendent()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What? You don't have the flower with you? You gave it away??? My research is slowly dying and you decided to give away the flower!");
                conversationList.Add("We need to continue research, we need to evolve, humans need to evolve and the only way is with science!");
                conversationList.Add("Agh I already had a bad feeling today, leave me alone I will need to think my next steps.");
            }
            else
            {
                conversationList.Add("Yes? I am busy... Oh it's you! Are you back with the flower??");
                conversationList.Add("YES! My research can continue at a fast pace again! Thank you so much.");
                conversationList.Add("I want to drive this village forward and you have helped me push it further! Thank you!");
                conversationList.Add("This flower can do so much! I can't be bothered to explain currently but hopefully in the future you will enjoy its benefits!");
                conversationList.Add("You have been really kind, now leave me to my research! I will move humanity forward!");
            }
        }
        else
        {
            conversationList.Add("Agh why has it been so difficult lately.");
            conversationList.Add("... Oh! I feel like my day is going to get better!");
            conversationList.Add($"My day is definitely better! {PlayerInteraction.Username} it is lovely to see you!");
            conversationList.Add("I certainly hope your day has been good. Mine has been a bit down but I am trying to get it back up.");
            conversationList.Add("People haven't been helping me out lately. Research has been quite slow due to that.");
            conversationList.Add("But I know a good buddy when I see one! How about helping me? After all this is for the future of the village!");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Please try to leave me be until you have it.");
        }
    }
    #endregion

    #region Neutral
    protected override void Neutral_New()
    {
        conversationList.Add("There... and then there...");
        conversationList.Add("What is it?? I am working here...");
        conversationList.Add($"Ah a new face... you look like a... hmm... yes exactly like a {PlayerInteraction.Username}.");
        conversationList.Add("Did I get it right? Of course I did.");
        conversationList.Add("Well, since you are here you may as well help me out.");
        conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
        conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
        conversationList.Add("Ah and don't bother me until you have it.");
    }


    protected override void Neutral_PlayerMiserable()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What? You had the flower but gave it away? I don't care for what reason you gave it away! That flower is really important!");
                conversationList.Add("Not good, this will take longer now! Seriously? Don't you understand how important science is??");
                conversationList.Add("Leave me alone, there is a lot of work for me to do now!");
            }
            else
            {
                conversationList.Add("Oh! You came back with the flower! Perfect! I really appreciate this.");
                conversationList.Add("This will help me breakthrough science! This flower is really special you see.");
                conversationList.Add("This flower has potent healing powers. However, we don't have the current tools to break down the flower properly.");
                conversationList.Add("I do also believe this flower can be used within dishes for better nutrition, even if highly poisonous.");
                conversationList.Add("I thank you again. I need to concentrate on my research now.");
            }

        }
        else
        {
            conversationList.Add("There... and then there...");
            conversationList.Add("AH! Please don't scare me like this next time!");
            conversationList.Add("Oh? Are you a new person? No... something tells me you are trouble.");
            conversationList.Add("Is this an unlucky day? Well I guess I should try using this opportunity for my own good.");
            conversationList.Add("How about being part of the future of research? I need you to bring me a little something.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Ah and don't bother me until you have it.");
        }
    }

    protected override void Neutral_PlayerUnhappy()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What? You had the flower but gave it away? I don't care for what reason you gave it away! That flower is really important!");
                conversationList.Add("Not good, this will take longer now! Seriously? Don't you understand how important science is??");
                conversationList.Add("Leave me alone, there is a lot of work for me to do now!");
            }
            else
            {
                conversationList.Add("Oh! You came back with the flower! Perfect! I really appreciate this.");
                conversationList.Add("This will help me breakthrough science! This flower is really special you see.");
                conversationList.Add("This flower has potent healing powers. However, we don't have the current tools to break down the flower properly.");
                conversationList.Add("I do also believe this flower can be used within dishes for better nutrition, even if highly poisonous.");
                conversationList.Add("I thank you again. I need to concentrate on my research now.");
            }

        }
        else
        {
            conversationList.Add("There... and then there...");
            conversationList.Add("What is it?? I am working here...");
            conversationList.Add("Oh hm apologies I can't remember your face too well. Oh well.");
            conversationList.Add("If I can't remember someone's face it means I am trying to forget them. Bet you aren't the nicest person are you?");
            conversationList.Add("Well if you do something nice for me I will try to remember you for next time.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Ah and don't bother me until you have it.");
        }
    }

    protected override void Neutral_PlayerNeutral()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What? You had the flower but gave it away? I don't care for what reason you gave it away! That flower is really important!");
                conversationList.Add("Not good, this will take longer now! Seriously? Don't you understand how important science is??");
                conversationList.Add("Leave me alone, there is a lot of work for me to do now!");
            }
            else
            {
                conversationList.Add("Oh! You came back with the flower! Perfect! I really appreciate this.");
                conversationList.Add("This will help me breakthrough science! This flower is really special you see.");
                conversationList.Add("This flower has potent healing powers. However, we don't have the current tools to break down the flower properly.");
                conversationList.Add("I do also believe this flower can be used within dishes for better nutrition, even if highly poisonous.");
                conversationList.Add("I thank you again. I need to concentrate on my research now.");
            }

        }
        else
        {
            conversationList.Add("There... and then there...");
            conversationList.Add("Hm what do you want?");
            conversationList.Add($"Ah what was your name again? It was... {PlayerInteraction.Username}. Yes, that was it.");
            conversationList.Add("Well, since you are here you may as well help me out.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Ah and don't bother me until you have it.");
        }
    }

    protected override void Neutral_PlayerHappy()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What? You had the flower but gave it away? I don't care for what reason you gave it away! That flower is really important!");
                conversationList.Add("Not good, this will take longer now! Seriously? Don't you understand how important science is??");
                conversationList.Add("Leave me alone, there is a lot of work for me to do now!");
            }
            else
            {
                conversationList.Add("Oh! You came back with the flower! Perfect! I really appreciate this.");
                conversationList.Add("This will help me breakthrough science! This flower is really special you see.");
                conversationList.Add("This flower has potent healing powers. However, we don't have the current tools to break down the flower properly.");
                conversationList.Add("I do also believe this flower can be used within dishes for better nutrition, even if highly poisonous.");
                conversationList.Add("I thank you again. I need to concentrate on my research now.");
            }

        }
        else
        {
            conversationList.Add("There... and then there...");
            conversationList.Add("Excuse me could you please NOT bother me...");
            conversationList.Add($"Oh its good to see you {PlayerInteraction.Username}. I did not realise it was you");
            conversationList.Add("Well, maybe the world destined for you to be here today to help me! How lovely! Researchs been kind of meh lately.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Don't bother me until you have the flower please. Thank you!");
        }
    }

    protected override void Neutral_PlayerTranscendent()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("What? You had the flower but gave it away? I don't care for what reason you gave it away! That flower is really important!");
                conversationList.Add("Not good, this will take longer now! Seriously? Don't you understand how important science is??");
                conversationList.Add("Leave me alone, there is a lot of work for me to do now!");
            }
            else
            {
                conversationList.Add("Oh! You came back with the flower! Perfect! I really appreciate this.");
                conversationList.Add("This will help me breakthrough science! This flower is really special you see.");
                conversationList.Add("This flower has potent healing powers. However, we don't have the current tools to break down the flower properly.");
                conversationList.Add("I do also believe this flower can be used within dishes for better nutrition, even if highly poisonous.");
                conversationList.Add("I thank you again. I need to concentrate on my research now.");
            }

        }
        else
        {
            conversationList.Add("There... and then there...");
            conversationList.Add("Could you PLEASE no- oh if it isn't you!");
            conversationList.Add($"How you feeling {PlayerInteraction.Username}? I am glad to see you!");
            conversationList.Add("Research hasn't really been moving much, but I know you are just the person to ask for help.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Don't bother me until you have the flower please. Thank you!");
        }
    }
    #endregion

    #region Happy
    protected override void Happy_New()
    {
        conversationList.Add("hmm... there we go... this is starting to look good!");
        conversationList.Add("If this continues it could go brilliantly!");
        conversationList.Add("Hm? I am currently working.");
        conversationList.Add($"Oh a new face... how lovely, let me guess... {PlayerInteraction.Username}?");
        conversationList.Add("Did I get it right? Of course I did.");
        conversationList.Add("Well, since you are here you may as well help me out.");
        conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
        conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
        conversationList.Add("Try to not bother me until you have it.");
    }

    protected override void Happy_PlayerMiserable()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("I assume you bring good news?");
                conversationList.Add("Sorry? Did I hear you right? You gave the flower away?? What made you do that! Actually no, I don't care!");
                conversationList.Add("Researched seemed to be finally blooming and you really decided it wasn't worth the while to help me continue the research? Seriously?");
                conversationList.Add("This village needs research! I want to drive this village forward!");
                conversationList.Add("Ugh, don't talk to me. I am seriously annoyed and just want to continue with my current research.");
            }
            else
            {
                conversationList.Add("I assume you bring good news?");
                conversationList.Add("Yes exactly the flower I wanted thank you very much!");
                conversationList.Add("The more I research this flower the better the health of this village will be. Who knows I might find a breakthrough!");
                conversationList.Add("Thank you for your efforts! I really appreciate it.");
                conversationList.Add("Now I have to focus on my research, thanks again.");
            }

        }
        else
        {
            conversationList.Add("Dadam.... dadam... dadam...");
            conversationList.Add("I was feeling quite lively today but there seems to be an omnious presence nearby.");
            conversationList.Add($"Could it be you? Hm... you do seem familiar. I don't like this feeling.");
            conversationList.Add("How about this, I clearly don't have a good feeling about you and I get the feeling you don't have a good feeling about me either.");
            conversationList.Add("How about you run a quick errand for me and who knows it might bring us in closer.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Try to not bother me until you have it.");
        }
    }

    protected override void Happy_PlayerUnhappy()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("I assume you bring good news?");
                conversationList.Add("Sorry? Did I hear you right? You gave the flower away?? What made you do that! Actually no, I don't care!");
                conversationList.Add("Researched seemed to be finally blooming and you really decided it wasn't worth the while to help me continue the research? Seriously?");
                conversationList.Add("This village needs research! I want to drive this village forward!");
                conversationList.Add("Ugh, don't talk to me. I am seriously annoyed and just want to continue with my current research.");
            }
            else
            {
                conversationList.Add("I assume you bring good news?");
                conversationList.Add("Yes exactly the flower I wanted thank you very much!");
                conversationList.Add("The more I research this flower the better the health of this village will be. Who knows I might find a breakthrough!");
                conversationList.Add("Thank you for your efforts! I really appreciate it.");
                conversationList.Add("Now I have to focus on my research, thanks again.");
            }

        }
        else
        {
            conversationList.Add("Dadam.... dadam... dadam...");
            conversationList.Add("It is feeling quite lively lately hasn't it?");
            conversationList.Add($"Ah hold I know you are... wait... I nearly got it...");
            conversationList.Add("Nevermind, can't remember you. Probably means you are annoying. Oh well, I hope you are having a good day just like me.");
            conversationList.Add("Perhaps if you are free you could run a quick errand for me.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Try to not bother me until you have it.");
        }
    }

    protected override void Happy_PlayerNeutral()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("I assume you bring good news?");
                conversationList.Add("Sorry? Did I hear you right? You gave the flower away?? What made you do that! Actually no, I don't care!");
                conversationList.Add("Researched seemed to be finally blooming and you really decided it wasn't worth the while to help me continue the research? Seriously?");
                conversationList.Add("This village needs research! I want to drive this village forward!");
                conversationList.Add("Ugh, don't talk to me. I am seriously annoyed and just want to continue with my current research.");
            }
            else
            {
                conversationList.Add("I assume you bring good news?");
                conversationList.Add("Yes exactly the flower I wanted thank you very much!");
                conversationList.Add("The more I research this flower the better the health of this village will be. Who knows I might find a breakthrough!");
                conversationList.Add("Thank you for your efforts! I really appreciate it.");
                conversationList.Add("Now I have to focus on my research, thanks again.");
            }

        }
        else
        {
            conversationList.Add("Dadam.... dadam... dadam...");
            conversationList.Add("Its a good day out there isn't it?");
            conversationList.Add($"Ah you are... {PlayerInteraction.Username}! Yes! that was it.");
            conversationList.Add("Someone that can help me out! An even better day today, how nice.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Try to not bother me until you have it.");
        }
    }

    protected override void Happy_PlayerHappy()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("I assume you bring good news?");
                conversationList.Add("Sorry? Did I hear you right? You gave the flower away?? What made you do that! Actually no, I don't care!");
                conversationList.Add("Researched seemed to be finally blooming and you really decided it wasn't worth the while to help me continue the research? Seriously?");
                conversationList.Add("This village needs research! I want to drive this village forward!");
                conversationList.Add("Ugh, don't talk to me. I am seriously annoyed and just want to continue with my current research.");
            }
            else
            {
                conversationList.Add("I assume you bring good news?");
                conversationList.Add("Yes exactly the flower I wanted thank you very much!");
                conversationList.Add("The more I research this flower the better the health of this village will be. Who knows I might find a breakthrough!");
                conversationList.Add("Thank you for your efforts! I really appreciate it.");
                conversationList.Add("Now I have to focus on my research, thanks again.");
            }

        }
        else
        {
            conversationList.Add("Dadam.... dadam... dadam...");
            conversationList.Add("Hm? Oh?");
            conversationList.Add($"{PlayerInteraction.Username}! How you doing? Cause I am having a good day!");
            conversationList.Add("Research has been running quite well lately, and you my friend can make it even better.");
            conversationList.Add("I trust you can run a quick errand for a good ol pal of yours.");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Try to not bother me until you have it.");
        }
    }

    protected override void Happy_PlayerTranscendent()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("I assume you bring good news?");
                conversationList.Add("Sorry? Did I hear you right? You gave the flower away?? What made you do that! Actually no, I don't care!");
                conversationList.Add("Researched seemed to be finally blooming and you really decided it wasn't worth the while to help me continue the research? Seriously?");
                conversationList.Add("This village needs research! I want to drive this village forward!");
                conversationList.Add("Ugh, don't talk to me. I am seriously annoyed and just want to continue with my current research.");
            }
            else
            {
                conversationList.Add("I assume you bring good news?");
                conversationList.Add("Yes exactly the flower I wanted thank you very much!");
                conversationList.Add("The more I research this flower the better the health of this village will be. Who knows I might find a breakthrough!");
                conversationList.Add("Thank you for your efforts! I really appreciate it.");
                conversationList.Add("Now I have to focus on my research, thanks again.");
            }

        }
        else
        {
            conversationList.Add("Dadam.... dadam... dadam...");
            conversationList.Add($"Here I feel the presence of my favourite person. Drum roll please!! {PlayerInteraction.Username}!");
            conversationList.Add($"It is absolutely lovely to see you here! My day has gotten even better.");
            conversationList.Add("Research has been running quite well lately and with you on my side I know it will run even better.");
            conversationList.Add("Your help has seriously helped my research and I still can't thank you enough for it!");
            conversationList.Add("You wouldn't mind running another quick errand for me will ya?");
            conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
            conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
            conversationList.Add("Try to not bother me until you have it.");
        }
    }
    #endregion

    #region Transcendent
    protected override void Transcendent_New()
    {
        conversationList.Add("Laaa... laaa... laaa...");
        conversationList.Add("This is going so well!");
        conversationList.Add("Welcome welcome. Ah a new face!");
        conversationList.Add($"No need to introduce yourself, let me guess... {PlayerInteraction.Username}?");
        conversationList.Add("Did I get it right? Of course I did. I always get it correct hahahahaha!");
        conversationList.Add("Well, since you are here you may as well help me out.");
        conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
        conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
    }

    protected override void Transcendent_PlayerMiserable()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Lalalalala!!");
                conversationList.Add("Ah you are back! I am sure you bring good news!");
                conversationList.Add("Sorry? Did I hear you right? You don't have the flower anymore?");
                conversationList.Add("...");
                conversationList.Add("Leave me alone... I can't believe you... I really don't want my good day to be ruined.");
            }
            else
            {
                conversationList.Add("Lalalalala!!");
                conversationList.Add("Ah you are back! I am sure you bring good news!");
                conversationList.Add("Excellent everything I need!");
                conversationList.Add("I am having the best day of my life at this rate! Thank you so much! This research will not only help the village but the future of humanity!");
                conversationList.Add("I really can't thank you enough! I will continue with my research now so please don't disturb me!");
                conversationList.Add("Lalala!! Lalalalaaaaa!!!");
            }

        }
        conversationList.Add("Laaa... laaa... laaa...");
        conversationList.Add("Such a wonderful day today yet there is an omnious presence in my vicinity.");
        conversationList.Add($"Wait... do I remember you? I really can't remember what your name is.");
        conversationList.Add($"Look whoever you are, my research has been the best it ever has been for a long time.");
        conversationList.Add("If I forget someone, it means there was a reason why I did so.");
        conversationList.Add("However, I may forgive you for whatever reason if you are willing to help me out.");
        conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
        conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
    }

    protected override void Transcendent_PlayerUnhappy()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Lalalalala!!");
                conversationList.Add("Ah you are back! I am sure you bring good news!");
                conversationList.Add("Sorry? Did I hear you right? You don't have the flower anymore?");
                conversationList.Add("...");
                conversationList.Add("Leave me alone... I can't believe you... I really don't want my good day to be ruined.");
            }
            else
            {
                conversationList.Add("Lalalalala!!");
                conversationList.Add("Ah you are back! I am sure you bring good news!");
                conversationList.Add("Excellent everything I need!");
                conversationList.Add("I am having the best day of my life at this rate! Thank you so much! This research will not only help the village but the future of humanity!");
                conversationList.Add("I really can't thank you enough! I will continue with my research now so please don't disturb me!");
                conversationList.Add("Lalala!! Lalalalaaaaa!!!");
            }

        }
        conversationList.Add("Laaa... laaa... laaa...");
        conversationList.Add("I am having a wonderful time, yet something is bugging me!");
        conversationList.Add($"Could it be you? Uh whoever you are.");
        conversationList.Add($"Research is going absolutely extraordinary here.");
        conversationList.Add("How about you help me... yea apologies about not knowing your name but hey! Being future of research can be fun.");
        conversationList.Add("I just need you to run a quick errand for me!");
        conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
        conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
    }

    protected override void Transcendent_PlayerNeutral()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Lalalalala!!");
                conversationList.Add("Ah you are back! I am sure you bring good news!");
                conversationList.Add("Sorry? Did I hear you right? You don't have the flower anymore?");
                conversationList.Add("...");
                conversationList.Add("Leave me alone... I can't believe you... I really don't want my good day to be ruined.");
            }
            else
            {
                conversationList.Add("Lalalalala!!");
                conversationList.Add("Ah you are back! I am sure you bring good news!");
                conversationList.Add("Excellent everything I need!");
                conversationList.Add("I am having the best day of my life at this rate! Thank you so much! This research will not only help the village but the future of humanity!");
                conversationList.Add("I really can't thank you enough! I will continue with my research now so please don't disturb me!");
                conversationList.Add("Lalala!! Lalalalaaaaa!!!");
            }

        }
        conversationList.Add("Laaa... laaa... laaa...");
        conversationList.Add("I am having a wonderful time, an absolute joyful time!");
        conversationList.Add($"{PlayerInteraction.Username} you better be having a good good time as well!");
        conversationList.Add($"Research is going absolutely extraordinary.");
        conversationList.Add("You know what I think would make this day the absolute best day? If you ran a quick errand for me.");
        conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
        conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
    }

    protected override void Transcendent_PlayerHappy()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Lalalalala!!");
                conversationList.Add("Ah you are back! I am sure you bring good news!");
                conversationList.Add("Sorry? Did I hear you right? You don't have the flower anymore?");
                conversationList.Add("...");
                conversationList.Add("Leave me alone... I can't believe you... I really don't want my good day to be ruined.");
            }
            else
            {
                conversationList.Add("Lalalalala!!");
                conversationList.Add("Ah you are back! I am sure you bring good news!");
                conversationList.Add("Excellent everything I need!");
                conversationList.Add("I am having the best day of my life at this rate! Thank you so much! This research will not only help the village but the future of humanity!");
                conversationList.Add("I really can't thank you enough! I will continue with my research now so please don't disturb me!");
                conversationList.Add("Lalala!! Lalalalaaaaa!!!");
            }

        }
        conversationList.Add("Laaa... laaa... laaa...");
        conversationList.Add($"{PlayerInteraction.Username} how wonderful to see you! I was just singing out loud!");
        conversationList.Add($"Its just, a researchers dream is to get ahead of their research. Currently, research has been absolutely booming!");
        conversationList.Add($"Research is going absolutely extraordinary.");
        conversationList.Add("You know what I think would make this day the absolute best day? If you ran a quick errand for me.");
        conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
        conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
    }

    protected override void Transcendent_PlayerTranscendent()
    {
        if (PlayerInteraction.hasFlower & hasInteractedAlready)
        {
            if (PlayerInteraction.hasGivenFlower)
            {
                conversationList.Add("Lalalalala!!");
                conversationList.Add("Ah you are back! I am sure you bring good news!");
                conversationList.Add("Sorry? Did I hear you right? You don't have the flower anymore?");
                conversationList.Add("...");
                conversationList.Add("Leave me alone... I can't believe you... I really don't want my good day to be ruined.");
            }
            else
            {
                conversationList.Add("Lalalalala!!");
                conversationList.Add("Ah you are back! I am sure you bring good news!");
                conversationList.Add("Excellent everything I need!");
                conversationList.Add("I am having the best day of my life at this rate! Thank you so much! This research will not only help the village but the future of humanity!");
                conversationList.Add("I really can't thank you enough! I will continue with my research now so please don't disturb me!");
                conversationList.Add("Lalala!! Lalalalaaaaa!!!");
            }

        }
        conversationList.Add("Laaa... laaa... laaa...");
        conversationList.Add($"If it isn't my best best best friend {PlayerInteraction.Username}! Oh how happy I am to see you!");
        conversationList.Add($"My research is just going phenominal! Thanks to your efforts too!");
        conversationList.Add($"Research can never stop! So please keep helping me out!");
        conversationList.Add("As always, I will need the same thing from you.");
        conversationList.Add("From the opposite side of where you came from there is another exit with a path leading to a flower.");
        conversationList.Add("This flower is quite extraordinary and needs further research on, if you could crab it and give it to me that be great.");
    }
    #endregion
}
