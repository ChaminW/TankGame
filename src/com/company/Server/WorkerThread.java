package com.company.Server;

import org.apache.commons.io.IOUtils;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;


public class WorkerThread extends Thread {

    private Socket socket;
    private int clientID;

     public WorkerThread(Socket socket, int clientID){
         this.clientID=clientID;
         this.socket=socket;
     }

    @Override
    public void run() {
        PrintWriter out=null;
        BufferedReader in=null;

        try {
            System.out.println("THis is inside thread");
            System.out.println("THis is handled by "+Thread.currentThread().getName());
            out=new PrintWriter(socket.getOutputStream(),true);
            in=new BufferedReader(new InputStreamReader(socket.getInputStream()));
            out.println("Welcome to the capitalization server");
            out.println("your client id is "+clientID);

            while(true){
                String input=in.readLine();
                System.out.println(input);
                if(input==null  || input.equals(".")){
                    break;
                }
                out.println(input.toUpperCase());
            }

        } catch (IOException e) {
            e.printStackTrace();
        }finally {
            IOUtils.closeQuietly(socket);
            IOUtils.closeQuietly(in);
            IOUtils.closeQuietly(out);
        }

    }
}
