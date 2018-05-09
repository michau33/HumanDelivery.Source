using UnityEngine;
public static class AudioUtility {
    public static AudioSource GetSource( GameObject obj, int priority ) {
        foreach( AudioSource source in obj.GetComponents<AudioSource>() ) {
            if( source.priority == priority ) {
                return source;
            } 
        }
        return null;
    }
}