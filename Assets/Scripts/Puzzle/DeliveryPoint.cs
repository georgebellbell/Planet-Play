using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to delivery points on puzzle planets
public class DeliveryPoint : MonoBehaviour
{
    [SerializeField] Color noObjectColour;
    [SerializeField] Color hasObjectColour;
    [SerializeField] ParticleSystem confettiPS;
    [SerializeField] AudioClip completedSoundEffect;

    Renderer renderer;
    PuzzleMode puzzleMode;

    bool objectDelivered = false;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        puzzleMode = FindObjectOfType<PuzzleMode>();

        renderer.material.color = noObjectColour;
    }

    // when cube object is pushed into delivery point, it gives positive feedback to player and
    // then calls function in PuzzleMode.CS to check if win is achieved
    private void OnTriggerEnter(Collider other)
    {
        renderer.material.color = hasObjectColour;
        objectDelivered = true;

        // upon delivering an object to the delivery point, a particle system effect will play as well as a sound effect
        confettiPS.Play();
        AudioSource.PlayClipAtPoint(completedSoundEffect, transform.position);
        puzzleMode.CheckAllPointsCovered(); 

    }

    // if cube object leaves the delivery point, it returns to its original state
    private void OnTriggerExit(Collider other)
    {
        renderer.material.color = noObjectColour;
        objectDelivered = false;
        puzzleMode.CheckAllPointsCovered();
    }

    // called by PuzzleMode.CS to check if all points have been activated
    public bool HasObject()
    {
        return objectDelivered;
    }

}
