<?php

namespace SoftUniBlogBundle\Controller;

use Sensio\Bundle\FrameworkExtraBundle\Configuration\Route;
use Symfony\Bundle\FrameworkBundle\Controller\Controller;

class SecurityController extends Controller
{
    /**
     * @Route("/login", name="security_login")
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function indexAction()
    {
        return $this->render("security/login.html.twig");
    }

    /**
     * @Route("/logout", name="security_logout")
     * @return \Symfony\Component\HttpFoundation\Response
     */
    public function logout()
    {
        //TODO
        return $this->redirectToRoute("blog_index");
    }
}
