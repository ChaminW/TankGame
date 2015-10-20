package com.company.Server;



import org.apache.commons.io.IOUtils;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Date;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;



public class Server {
    private static int clientCout=0;

    public static void main(String args[]){
        ExecutorService executorService= Executors.newCachedThreadPool();
         //there are other types of thrad pools also.this doesnt have any threads at the begining.when client connects it creates a thread and add to thread pool

        ServerSocket serversocket=null;
        Socket socket=null;

        try {
           serversocket =new ServerSocket(9090);
            System.out.println("server is running");
           while(true){
//               socket= serversocket.accept();  //ctrl+atl+v to auto asign value to a variable
//               System.out.println("client connectd");
//               ClientHandler clientHandler=new ClientHandler(socket,clientCout++);
//               clientHandler.start();

               Runnable worker=new WorkerThread(serversocket.accept(),clientCout++);
               executorService.execute(worker);
           }




        } catch (IOException e) {
            e.printStackTrace();
        }finally{
            IOUtils.closeQuietly(serversocket);

        }

    }
}
