"use client"

import Image from "next/image"
import { Button } from "@/components/ui/button"
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog"

interface TileGameDialogProps {
  matrix: number[][]
}

export function TileGameDialog({ matrix }: TileGameDialogProps) {
  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button variant="outline">Verify CAPTCHA</Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[800px]">
        <DialogHeader>
          <DialogTitle className="text-2xl sm:text-3xl font-bold text-center">Tile Game</DialogTitle>
        </DialogHeader>
        <div className="w-full bg-white rounded-lg overflow-hidden flex flex-col md:flex-row">
          <div className="flex-1 p-4 sm:p-6 flex flex-col">
            <div className="mb-4 sm:mb-6 rounded-md overflow-hidden">
              <Image
                src="https://picsum.photos/200/300"
                alt="Placeholder Image"
                width={400}
                height={300}
                className="w-full h-[200px] sm:h-[250px] md:h-[300px] object-cover"
              />
            </div>
            <div className="flex gap-2 sm:gap-4 mt-auto">
              <Button className="flex-1">Submit</Button>
              <Button variant="outline" className="flex-1">
                Refresh
              </Button>
            </div>
          </div>
          <div className="flex-1 border-t md:border-l md:border-t-0 border-gray-200 p-4 sm:p-6">
            <h2 className="text-xl sm:text-2xl font-bold mb-2 sm:mb-4">Matrix Buttons</h2>
            <div className="flex flex-col gap-2">
              {matrix.map((row, rowIndex) => (
                <div key={rowIndex} className="flex gap-2">
                  {row.map((value, colIndex) => (
                    <Button
                      key={`${rowIndex}-${colIndex}`}
                      variant={value === 0 ? "outline" : "default"}
                      className="w-10 h-10 sm:w-12 sm:h-12"
                    >
                      {value}
                    </Button>
                  ))}
                </div>
              ))}
            </div>
          </div>
        </div>
      </DialogContent>
    </Dialog>
  )
}

