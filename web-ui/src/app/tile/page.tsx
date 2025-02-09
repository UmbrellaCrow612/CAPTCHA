"use client";

import { useEffect, useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Card, CardContent } from "@/components/ui/card";
import { TileGameDialog } from "@/components/our/TileGameDialog";

export default function Page() {
  const [captchaData, setCaptchaData] = useState<{
    baseMatrix: number[][];
    imageUrl: string;
    captchaId: string;
  }>({ baseMatrix: [[]], captchaId: "", imageUrl: "" });

  useEffect(() => {
    async function fetchCaptcha() {
      try {
        const response = await fetch("https://localhost:7153/captcha/tile", {
          method: "GET",
          headers: {
            Accept: "image/png",
          },
        });

        if (!response.ok) {
          throw new Error("Failed to fetch CAPTCHA");
        }

        const blob = await response.blob();
        const imageUrl = URL.createObjectURL(blob);
        console.log("Img " + imageUrl);
        const _matrix = response.headers.get("x-base-matrix");
        const baseMatrix = JSON.parse(_matrix ?? "[]");
        console.log("Matrix " + baseMatrix);
        const captchaId = response.headers.get("x-captcha-id") ?? "Null";
        console.log("Cap ID " + captchaId);

        setCaptchaData({
          imageUrl,
          baseMatrix,
          captchaId,
        });
      } catch (error) {
        console.error("Error fetching CAPTCHA:", error);
      }
    }

    fetchCaptcha();
  }, []);

  return (
    <div className="min-h-screen w-full flex justify-center items-center bg-gray-50 p-4">
      <Card className="w-full max-w-md p-6 shadow-xl bg-white">
        <CardContent className="space-y-4">
          <h1 className="text-2xl font-bold text-center text-gray-800">
            Sign Up
          </h1>
          <div>
            <Label htmlFor="email">Email</Label>
            <Input id="email" type="email" placeholder="Enter your email" />
          </div>
          <div>
            <Label htmlFor="password">Password</Label>
            <Input
              id="password"
              type="password"
              placeholder="Enter your password"
            />
          </div>
          {captchaData && <TileGameDialog captchaData={captchaData} />}
          <Button className="w-full">Submit</Button>
        </CardContent>
      </Card>
    </div>
  );
}
