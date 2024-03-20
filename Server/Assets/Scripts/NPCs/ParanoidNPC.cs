using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParanoidNPC : NPCBehaviour
{
    
    public override NPCBehaviour StartConversation()
    {
        if (playerTaskFinished)
        {
            conversationList.Clear();
            return this;
        }
        if (hasInteractedAlready)
        {
            PlayerMakeNPCSadder(1);
            playerTaskFinished = true;
        }
        base.StartConversation();
        return this;
    }

    public override void OnClientDisconnected(ulong clientID)
    {
        if (!playerTaskFinished && hasInteractedAlready) PlayerMakeNPCHappier(1);
        base.OnClientDisconnected(clientID);
    }


    #region Miserable
    protected override void Miserable_New()
    {
        conversationList.Add("O-oh, I haven't seen you here before. No never, I am sure of it yes...");
        conversationList.Add("Wha-what's your name?");
        conversationList.Add($"{PlayerInteraction.Username}? wh-what a nice name! My name is Para! N-nice to meet you!");
        conversationList.Add("I a-am having a terrible time sorry... I d-don't like having m-multiple conversations with people b-but they keep trying t-to t-alk to me.");
        conversationList.Add("Would you p-please n-not talk to me again? Maybe i-in the future we c-can but please... not now");
    }

    protected override void Miserable_PlayerMiserable()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("I am having the w-worst time of m-my life and of c-course you take this a-as the b-best opportunity to r-ruin my day further.");
            conversationList.Add("I didn't expect any less from y-you!");
        }
        else
        {
            conversationList.Add("...");
            conversationList.Add("Oh... sorry I didn't realise you were there... I a-am having a r-really bad time...");
            conversationList.Add("P-please I b-beg j-just leave me alone u-until your next visit... p-please...");
        }
    }

    protected override void Miserable_PlayerUnhappy()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("I am having a r-really difficult time and I a-asked you spe-specifically to leave me alone!");
            conversationList.Add("Please... for the rest of t-the day l-leave me alone...");
        }
        else
        {
            conversationList.Add("...");
            conversationList.Add($"Oh... {PlayerInteraction.Username}... hey, I didn't realise you were there...");
            conversationList.Add("I have been feeling really bad lately and I'd rather be left alone... y-you can talk to me in your next visit...");
        }
    }

    protected override void Miserable_PlayerNeutral()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("W-why are you a-also disrespecting me...");
            conversationList.Add("S-seems like I w-won't be catching a break...");
            conversationList.Add("Please... leave me a-alone...");
        }
        else
        {
            conversationList.Add("...");
            conversationList.Add($"Oh hey {PlayerInteraction.Username}... sorry been feeling a bit down b-but I s-should be fine...");
            conversationList.Add($"Other people have j-just been disrespecting me...");
            conversationList.Add("I am feeling really bad and I'd rather be left alone... y-you can talk to me in your next visit...");
        }
    }

    protected override void Miserable_PlayerHappy()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("I... I t-thought I could finally t-trust you...");
            conversationList.Add("I f-feel even w-worse now...");
            conversationList.Add("P-please leave me be...");
        }
        else
        {
            conversationList.Add($"Oh... hey {PlayerInteraction.Username}... didn't realise you were there at first... sorry been feeling a bit down.");
            conversationList.Add($"Other people have j-just been disrespecting me, I d-don't know why either I p-promise I am not doing anything but a-asking them to l-leave me be...");
            conversationList.Add("C-could y-you talk to me a-again the n-next time you come v-visit? I n-need some time a-alone... I w-would really appreciate that...");
        }
    }

    protected override void Miserable_PlayerTranscendent()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("I a-am at m-my worst and I t-truly trusted you... w-why did y-you do this...? I... I trusted you...");
        }
        else
        {
            conversationList.Add($"Oh Hey {PlayerInteraction.Username}. H-how are you...");
            conversationList.Add("Sorry I have b-been feeling really down l-lately... people have b-been continously disrespecting my request.");
            conversationList.Add("I do f-feel a bit happier seeing you... I d-do trust you...");
            conversationList.Add("C-could you l-like always talk to me again o-once you next come visit the village? I w-would really appreciate t-that!");
        }
    }
    #endregion

    #region Unhappy
    protected override void Unhappy_New()
    {
        conversationList.Add("O-oh, I haven't seen you here before. No never, I am sure of it yes...");
        conversationList.Add("Wha-what's your name?");
        conversationList.Add($"{PlayerInteraction.Username}? wh-what a nice name! My name is Para! N-nice to meet you!");
        conversationList.Add($"People have been q-quite annoying lately... b-but I t-trust that you are a nice person.");
        conversationList.Add($"I d-don't like when people try to have m-multiple conversations with me... so if you w-want to talk to me again please do it n-next time you are in the village...");
    }

    protected override void Unhappy_PlayerMiserable()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("O-of course you take your c-chance to annoy me... j-just g-go a-away!");
        }
        else
        {
            conversationList.Add("...");
            conversationList.Add("Oh, s-sorry didn't realise you w-were there...");
            conversationList.Add("P-please don't a-annoy me by trying to talk to me again... I haven't been having a g-good time...");
            conversationList.Add("I r-really want some time a-alone.");
        }
    }

    //do this and beneath once you are back
    protected override void Unhappy_PlayerUnhappy()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("Y-you r-really are j-just like everybody else...");
            conversationList.Add("D-do you enjoy worsening my d-day?");
        }
        else
        {
            conversationList.Add($"Oh h-hey {PlayerInteraction.Username}. I a-am currently not feeling the best...");
            conversationList.Add($"I-if you don't mind just letting me be in peace to t-that be great... please...");
            conversationList.Add("I think I n-need some extra time a-alone.");
        }
    }

    protected override void Unhappy_PlayerNeutral()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("W-why did you talk t-to me again I specifically r-requested you not too...");
            conversationList.Add("Just leave me be f-for the rest of the day...");
        }
        else
        {
            conversationList.Add("hm....");
            conversationList.Add($"O-oh hey {PlayerInteraction.Username}, didn't s-see you there...");
            conversationList.Add("I r-really want some time a-alone. P-people have been a-annoying me lately...");
            conversationList.Add("J-just talk to me again n-next time you v-vist just p-please not right n-now.");
        }
    }

    protected override void Unhappy_PlayerHappy()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("I w-was sad b-but now I a-am sadder... c-couldn't you just have done what I a-asked of you?");
        }
        else
        {
            conversationList.Add($"H-hey {PlayerInteraction.Username} h-how are you doing? M-me? I could be d-doing better...");
            conversationList.Add("P-people haven't been nice t-to me l-lately but I p-promise its not like I did a-anything! All I a-ask is for them to n-not talk to me, maybe another time...");
            conversationList.Add("Yet they always ignore m-my request... m-mind if you could follow it? J-just let me go by my day until y-you next time c-come and visit...");
        }
    }

    protected override void Unhappy_PlayerTranscendent()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("O-of course you take your c-chance to annoy me... j-just g-go a-away!");
        }
        else
        {
            conversationList.Add($"Oh h-hey {PlayerInteraction.Username}! S-sorry I was lost in t-thought.");
            conversationList.Add("I have been f-feeling a bit d-down lately... not m-many people seem to have been respecting m-me lately...");
            conversationList.Add("H-hey I t-trust you quite a l-lot so I b-believe you will r-respect me.");
            conversationList.Add("J-just please as a-always w-we can talk next time you c-come and visit.");
        }
    }
    #endregion

    #region Neutral
    protected override void Neutral_New()
    {
        conversationList.Add("O-oh, I haven't seen you here before. No never, I am sure of it yes...");
        conversationList.Add("Wha-what's your name?");
        conversationList.Add($"{PlayerInteraction.Username}? wh-what a nice name! My name is Para! N-nice to meet you!");
        conversationList.Add("J-just so you know I am n-not the biggest fan of having m-multiple conversations s-so please refrain from s-speaking again until the n-near future...");
        conversationList.Add("T-take care now...");
    }

    protected override void Neutral_PlayerMiserable()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("I t-thought I may finally have a good day but of course yo-you decided to annoy me like always.");
            conversationList.Add("W-why do you keep doing this?");
        }
        else
        {
            conversationList.Add("Pl-please don't talk to m-me, it-its not funny. Y-you never listen to me... p-please.");
        }
    }

    protected override void Neutral_PlayerUnhappy()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("I t-thought I may finally have a good day but I g-guess you still don't understand...");
            conversationList.Add("I'm n-not the biggest f-fan of talking. Please r-respect that...");
        }
        else
        {
            conversationList.Add($"{PlayerInteraction.Username} if... if you don't mind could you learn to n-not speak to me after our i-initial conversation?");
            conversationList.Add($"I d-don't mind talking for a bit, especially if w-we haven't seen... each other for a while. But after p-please leave me alone.");
        }
    }

    protected override void Neutral_PlayerNeutral()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("W-why are you talking t-to me again I asked y-you specifically t-to not talk to me again.");
            conversationList.Add("N-now I am a b-bit annoyed.");
        }
        else
        {
            conversationList.Add($"Oh {PlayerInteraction.Username} hello... again... I hope you a-are all well... life currently has b-been q-quite average.");
            conversationList.Add("I hope y-you are doing well...");
            conversationList.Add("...");
            conversationList.Add("O-Okay... that is it... please l-leave me alone...");
        }
    }

    protected override void Neutral_PlayerHappy()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("W-why are you talking t-to me, I t-thought you understood that I d-don't like m-multiple conversations.");
            conversationList.Add("Please don't do it again...");
        }
        else
        {
            conversationList.Add($"Oh {PlayerInteraction.Username} hello... again... I hope you a-are all well...");
            conversationList.Add("Y-you are a nice p-person to me... t-thank you for that...");
            conversationList.Add("L-like usual p-please leave m-me alone...");
        }
    }

    protected override void Neutral_PlayerTranscendent()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("W-what why do y-you want to t-talk to me again! All I did w-was being nice, I t-thought you respected me...");
            conversationList.Add("... M-maybe I was w-wrong about you...?");
        }
        else
        {
            conversationList.Add($"Oh {PlayerInteraction.Username}! I a-am actually h-happy to see you. I t-think you are a really g-good person");
            conversationList.Add("H-hope you have a w-wonderful time h-here.");
            conversationList.Add("L-like usual p-please leave m-me alone...");
        }
    }
    #endregion

    #region Happy
    protected override void Happy_New()
    {
        conversationList.Add("O-oh, I haven't seen you here before. No never, I am sure of it yes...");
        conversationList.Add("Wha-what's your name?");
        conversationList.Add($"{PlayerInteraction.Username}? wh-what a nice name! My name is Para! N-nice to meet you!");
        conversationList.Add("I am currently doing w-well. Nothing b-bad has currently happened and o-other people seem to r-respect me.");
        conversationList.Add("Just s-so you know I d-don't like having multiple co-con-conversations with people...");
        conversationList.Add("I-if you don't mind, p-please dont t-talk to me again that be appreciated... or I will g-get sad...");
        conversationList.Add("M-maybe we can talk in the future w-who knows... see you later.");
    }

    protected override void Happy_PlayerMiserable()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("W-why are y-you trying to ruin my g-good days too?");
            conversationList.Add("J-just leave me alone already...");
        }
        else
        {
            conversationList.Add($"Oh... hello... {PlayerInteraction.Username}... I... I am h-having a good day... please don't ruin it...");
            conversationList.Add("Please r-respect me and l-leave me alone... thank you...");
            conversationList.Add("Go... go b-bother some-someone else!");
        }
    }

    protected override void Happy_PlayerUnhappy()
    {
        if (hasInteractedAlready)
        {   
            conversationList.Add("I w-was finally getting happier w-with the r-respect I was getting...");
            conversationList.Add("Is... is this funny to y-you? B-because it isn't to me!");
        }
        else
        {
            ConversationList.Add($"Oh hello {PlayerInteraction.Username}... how are you...? I a-am h-having a good day...");
            conversationList.Add("The other p-people h-have been quite nice t-to me it lately.");
            conversationList.Add("P-please don't t-talk to me... you know how it m-makes me feel!");
        }
    }

    protected override void Happy_PlayerNeutral()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("C-could you not have just done what I a-asked you to do...?");
            conversationList.Add("People w-were being so nice... why r-ruin it f-for me...");
        }
        else
        {
            conversationList.Add($"Hey {PlayerInteraction.Username} how are y-you doing? Myself? I am f-feeling well.");
            conversationList.Add($"P-people have been nice to me... it has been f-fun, I h-hope people continue being nice to me!");
            conversationList.Add($"If you d-dont mind... is it okay if y-you leave me alone until we m-meet again? Th-thanks!");
        }
    }
    protected override void Happy_PlayerHappy()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("I d-didn't expect you to not listen to m-me... I t-thought I could f-finally trust you...");
            conversationList.Add("Please, d-don't try to do that again...");
        }
        else
        {
            conversationList.Add("Hello again, th-things have been going well!");
            conversationList.Add("I t-think people are starting to like me and r-respect me.");
            conversationList.Add("If you c-could do what everyone else has been doing that be great... I b-believe you will! T-thanks!");
        }
    }

    protected override void Happy_PlayerTranscendent()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("Y-you... why? Y-you were the l-last person I expected to try a-annoying me...");
            conversationList.Add("G-guess you really can't trust annyone these days...");
        }
        else
        {
            conversationList.Add($"Hello {PlayerInteraction.Username}! I-it seems I people are being r-really nice to me.");
            conversationList.Add("H-hopefully this will streak w-will continue!");
            conversationList.Add("A-as always I w-will talk to you somewhere in the n-near future a-again!");
            conversationList.Add("T-thank you for always being super n-nice to me...");
        }
    }
    #endregion

    #region Transcendent
    protected override void Transcendent_New()
    {
        conversationList.Add("O-oh, I haven't seen you here before. No never, I am sure of it yes...");
        conversationList.Add("Wha-what's your name?");
        conversationList.Add($"{PlayerInteraction.Username}? wh-what a nice name! My name is Para! N-nice to meet you!");
        conversationList.Add($"I am g-glad to be meeting someone n-new! I am q-quite excited people h-have been treating m-me really nicely!");
        conversationList.Add($"S-see I a-am not the most social person and try to k-keep social interaction to a minimum, so I like to n-not have multiple conversation with p-eople...");
        conversationList.Add($"N-next time you are here d-do talk to m-me but not r-right now if that is okay!");
    }

    protected override void Transcendent_PlayerMiserable()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("Why why why e-even at my happiest m-moments you disrespect me...");
            conversationList.Add("Just stay away... it isn't funny...");
        }
        else
        {
            conversationList.Add($"OH... {PlayerInteraction.Username} it's you... hello...");
            conversationList.Add("Y-you always annoy me... b-but recently everyone has been super d-duper respectful!");
            conversationList.Add("So I b-beg of you! Please don't annoy me!");
            conversationList.Add("J-just leave me alone, don't t-talk to me!");
        }
    }

    protected override void Transcendent_PlayerUnhappy()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("C-couldn't you have at least tried being nice? I was super happy just now...");
            conversationList.Add("Just leave me alone for the rest of the day...");
        }
        else
        {
            conversationList.Add($"Oh {PlayerInteraction.Username} hello...");
            conversationList.Add("Y-you always annoy me... b-but recently everyone has been super d-duper respectful!");
            conversationList.Add("So I b-beg of you! Please don't annoy me!");
            conversationList.Add("J-just leave me alone, don't t-talk to me!");
        }
    }

    protected override void Transcendent_PlayerNeutral()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("I w-was in such a good mood today and I a-all I asked for w-was for a simple request... w-why did you have to do that?");
            conversationList.Add("P-please in the future do not t-talk to me again... i-it's alright...");
        }
        else
        {
            conversationList.Add($"H-hey {PlayerInteraction.Username}! I am d-doing q-quite well lately, what about you?");
            conversationList.Add($"P-people that have come v-visiting have been s-so respectful of my request. I r-really do thank them for that.");
            conversationList.Add($"B-but do understand even if I am in a g-good mood that doesn't mean you should t-talk to me again! J-just talk to me next time you come visit okay!");
        }
    }
    protected override void Transcendent_PlayerHappy()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("I a-am not really sure why y-you thought t-to talk to me a-again when I ask not to be t-talked to again.");
            conversationList.Add("It is annoying s-so please d-don't do it again!");
        }
        else
        {
            conversationList.Add($"Hey {PlayerInteraction.Username}! How have y-you been?");
            conversationList.Add("I have been d-doing really well! People v-visiting have been quite kind.");
            conversationList.Add("I hope this treak of k-kindness continues...");
            conversationList.Add("If y-you don't mind, like always to l-let me stay in p-peace and t-talk to me in your next visit!");
            conversationList.Add("I h-hope you have a good time!");
        }
    }
    protected override void Transcendent_PlayerTranscendent()
    {
        if (hasInteractedAlready)
        {
            conversationList.Add("W-w-why? I k-know I am in a good m-mood right now a-and I do t-thank you for always b-being so nice b-but...");
            conversationList.Add("I can't make any exceptions s-sorry, but i-it's okay j-just don't do it next time!");
        }
        else
        {
            conversationList.Add($"OH {PlayerInteraction.Username} you are back! I am Glad! How have y-you been?");
            conversationList.Add("Me? O-oh ye I have been d-doing really well! Thank you! E-everyone has been so lovely and r-respecting my request!");
            conversationList.Add($"I wanted to thank you {PlayerInteraction.Username} y-you have been very k-kind to me lately.");
            conversationList.Add("If y-you don't mind, like always to l-let me stay in p-peace and t-talk to me in your next visit!");
            conversationList.Add("I h-hope you have a good time!");
        }
    }
    #endregion
}

