"use client";

import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Card, CardContent } from "@/components/ui/card";
import { TileGameDialog } from "@/components/our/TileGameDialog";

export default function Page() {
  const matrix = [
    [1, 0, 0, 0],
    [0, 0, 0, 0],
  ];

  return (
    <div className="min-h-screen w-full flex justify-center items-center bg-gray-50 p-4">
      <Card className="w-full max-w-md p-6 shadow-xl bg-white">
        <CardContent className="space-y-4">
          <h1 className="text-2xl font-bold text-center text-gray-800">Sign Up</h1>
          <div>
            <Label htmlFor="email">Email</Label>
            <Input id="email" type="email" placeholder="Enter your email" />
          </div>
          <div>
            <Label htmlFor="password">Password</Label>
            <Input id="password" type="password" placeholder="Enter your password" />
          </div>
          <TileGameDialog matrix={matrix} />
          <Button className="w-full">Submit</Button>
        </CardContent>
      </Card>
    </div>
  );
}
